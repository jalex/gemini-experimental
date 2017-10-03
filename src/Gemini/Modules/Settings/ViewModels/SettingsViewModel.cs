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
        private string _toSelectPath, _toSelectName;

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
                    var path = settingsEditor.SettingsPagePath;
                    ExtractOrderFromName(ref path);
                    var name = settingsEditor.SettingsPageName;
                    var order = ExtractOrderFromName(ref name);
                    page = new SettingsPageViewModel {
                        Path = path,
                        Name = name,
                        Order = order
                    };
                    parentCollection.Add(page);
                }

                page.Editors.Add(settingsEditor);
            }
            SortAndTrimExcess(pages);

            Pages = pages;
            SelectedPage = GetFirstLeafPageRecursive(Pages);
        }

        //protected override void OnViewAttached(object view, object context) {
        //    base.OnViewAttached(view, context);
        //}
        protected override void OnViewLoaded(object view) {
            base.OnViewLoaded(view);

            NotifyIsVisible(Pages);

            foreach(var p in Pages.Where(p => p.IsVisible)) p.Load();

            if(_toSelectPath != null || _toSelectName != null) {
                var page = FindFirstPage(Pages, path: _toSelectPath, name: _toSelectName);
                if(page != null && page.IsVisible) SelectedPage = page;
                _toSelectPath = null;
                _toSelectName = null;
            }

            if(SelectedPage == null || !SelectedPage.IsVisible) SelectedPage = GetFirstLeafPageRecursive(Pages);
        }

        public void SelectPage(string path = null, string name = null) {
            _toSelectPath = path;
            _toSelectName = name;
        }

        private static void NotifyIsVisible(List<SettingsPageViewModel> pages) {
            foreach(var p in pages) {
                p.NotifyOfPropertyChange(nameof(p.IsVisible));
                NotifyIsVisible(p.Children);
            }
        }

        private static SettingsPageViewModel FindFirstPage(List<SettingsPageViewModel> pages, string path = null, string name = null, bool withEditors = true) {
            foreach(var p in pages.Where(p => p.IsVisible)) {
                if((!withEditors || p.Editors.Any(e => e.IsVisible)) &&
                   (path == null || string.Equals(p.Path, path, StringComparison.CurrentCultureIgnoreCase)) &&
                   (name == null || string.Equals(p.Name, name, StringComparison.CurrentCultureIgnoreCase))) {
                    return p;
                }
                var cp = FindFirstPage(p.Children, path: path, name: name, withEditors: withEditors);
                if(cp != null) return cp;
            }
            return null;
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
