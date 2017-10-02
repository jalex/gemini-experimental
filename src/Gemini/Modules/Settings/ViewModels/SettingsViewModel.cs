#region

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Caliburn.Micro;
using Gemini.Framework;
using Gemini.Properties;

#endregion

namespace Gemini.Modules.Settings.ViewModels
{
    [Export(typeof(SettingsViewModel))]
    public class SettingsViewModel : WindowBase
    {
        private static readonly Regex _OrderFromNameRE = new Regex(@"^\[(\d+)] (.*)$");
        private SettingsPageViewModel _selectedPage;
        private IEnumerable<ISettingsEditor> _settingsEditors;

        public List<SettingsPageViewModel> Pages { get; private set; }

        public SettingsPageViewModel SelectedPage
        {
            get { return _selectedPage; }
            set
            {
                _selectedPage = value;
                NotifyOfPropertyChange(() => SelectedPage);
            }
        }

        public ICommand CancelCommand { get; }
        public ICommand OkCommand { get; }

        public SettingsViewModel()
        {
            CancelCommand = new RelayCommand(o => TryClose(false));
            OkCommand = new RelayCommand(SaveChanges);

            DisplayName = Resources.SettingsDisplayName;
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            var pages = new List<SettingsPageViewModel>();
            _settingsEditors = IoC.GetAll<ISettingsEditor>();

            foreach (var settingsEditor in _settingsEditors)
            {
                var parentCollection = GetParentCollection(settingsEditor, pages);

                var page =
                    parentCollection.FirstOrDefault(m => m.Name == settingsEditor.SettingsPageName);

                if (page == null)
                {
                    var name = settingsEditor.SettingsPageName;
                    var order = ExtractOrderFromName(ref name);
                    page = new SettingsPageViewModel
                    {
                        Name = name,
                        Order = order
                    };
                    parentCollection.Add(page);
                }

                page.Editors.Add(settingsEditor);
            }
            SortAndTrimExcess(pages);

            Pages = pages;
            SelectedPage = GetFirstLeafPageRecursive(pages);
        }

        private static int ExtractOrderFromName(ref string name)
        {
            var m = _OrderFromNameRE.Match(name);
            if (m.Success)
            {
                name = m.Groups[2].Value;
                return int.Parse(m.Groups[1].Value);
            }
            return 0;
        }

        private static void SortAndTrimExcess(List<SettingsPageViewModel> pages)
        {
            if (pages.Count > 1)
                pages.Sort((p1, p2) =>
                {
                    var r = p1.Order.CompareTo(p2.Order);
                    if (r == 0) r = p1.Name.CompareTo(p2.Name);
                    return r;
                });
            foreach (var p in pages)
            {
                SortAndTrimExcess(p.Children);
                if (p.Editors.Count > 1)
                {
                    p.Editors.Sort((e1, e2) => (e1 is ISettingsEditorOrder ? ((ISettingsEditorOrder) e1).Order : 0)
                        .CompareTo(e2 is ISettingsEditorOrder ? ((ISettingsEditorOrder) e2).Order : 0));
                    p.Editors.TrimExcess();
                }
            }
            pages.TrimExcess();
        }

        private static SettingsPageViewModel GetFirstLeafPageRecursive(List<SettingsPageViewModel> pages)
        {
            if (!pages.Any(p => p.IsVisible))
                return null;

            var firstPage = pages.First(p => p.IsVisible);
            if (!firstPage.Children.Any(p => p.IsVisible))
                return firstPage;

            return GetFirstLeafPageRecursive(firstPage.Children);
        }

        private List<SettingsPageViewModel> GetParentCollection(ISettingsEditor settingsEditor,
            List<SettingsPageViewModel> pages)
        {
            if (string.IsNullOrEmpty(settingsEditor.SettingsPagePath))
                return pages;

            var path = settingsEditor.SettingsPagePath.Split(new[] {'\\'}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var pathElement in path)
            {
                var page = pages.FirstOrDefault(s => s.Name == pathElement);

                if (page == null)
                {
                    var name = pathElement;
                    var order = ExtractOrderFromName(ref name);
                    page = new SettingsPageViewModel
                    {
                        Name = name,
                        Order = order
                    };
                    pages.Add(page);
                }

                pages = page.Children;
            }

            return pages;
        }

        private void SaveChanges(object obj)
        {
            foreach (var settingsEditor in _settingsEditors.Where(e => e.IsVisible))
                settingsEditor.ApplyChanges();

            TryClose(true);
        }
    }
}
