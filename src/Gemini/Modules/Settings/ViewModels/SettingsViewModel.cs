#region

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
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
        private SettingsPageViewModel _selectedPage;
        private IEnumerable<ISettingsEditor> _settingsEditors;

        public SettingsViewModel()
        {
            CancelCommand = new RelayCommand(o => TryClose(false));
            OkCommand = new RelayCommand(SaveChanges);

            DisplayName = Resources.SettingsDisplayName;
        }

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

        public ICommand CancelCommand { get; private set; }
        public ICommand OkCommand { get; private set; }

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
                    page = new SettingsPageViewModel
                    {
                        Name = settingsEditor.SettingsPageName
                    };
                    parentCollection.Add(page);
                }

                page.Editors.Add(settingsEditor);
            }

            Pages = pages;
            SelectedPage = GetFirstLeafPageRecursive(pages);
        }

        private static SettingsPageViewModel GetFirstLeafPageRecursive(List<SettingsPageViewModel> pages)
        {
            if (!pages.Any())
                return null;

            var firstPage = pages.First();
            if (!firstPage.Children.Any())
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
                    page = new SettingsPageViewModel {Name = pathElement};
                    pages.Add(page);
                }

                pages = page.Children;
            }

            return pages;
        }

        private void SaveChanges(object obj)
        {
            foreach (var settingsEditor in _settingsEditors)
                settingsEditor.ApplyChanges();

            TryClose(true);
        }
    }
}