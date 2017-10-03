#region

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using Caliburn.Micro;
using Gemini.Framework.Themes;
using Gemini.Modules.Settings;
using Gemini.Properties;

#endregion

namespace Gemini.Modules.MainMenu.ViewModels
{
    [Export(typeof(ISettingsEditor))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MainMenuSettingsViewModel : PropertyChangedBase, ISettingsEditor
    {
        private static readonly List<string> AvailableLanguages = new List<string>
        {
            string.Empty,
            "en",
            "de",
            "ru",
            "zh-Hans",
            "ko"
        };

        private readonly IThemeManager _themeManager;
        private bool _autoHideMainMenu;
        private string _selectedLanguage;

        private ITheme _selectedTheme;

        public IEnumerable<ITheme> Themes => _themeManager.Themes;

        public ITheme SelectedTheme
        {
            get { return _selectedTheme; }
            set
            {
                if (value.Equals(_selectedTheme)) return;
                _selectedTheme = value;
                NotifyOfPropertyChange(() => SelectedTheme);
            }
        }

        public bool ThemeAllowEditing {
            get {
                var s = ConfigurationManager.AppSettings["Gemini_Settings_ThemeName_PreventEditing"];
                if(s == null || !bool.TryParse(s, out var r)) return true;
                return !r;
            }
        }

        public IEnumerable<string> Languages => AvailableLanguages;

        public string SelectedLanguage
        {
            get { return _selectedLanguage; }
            set
            {
                if (value.Equals(_selectedLanguage))
                    return;
                _selectedLanguage = value;
                NotifyOfPropertyChange(() => SelectedLanguage);
            }
        }

        public bool LanguageAllowEditing {
            get {
                var s = ConfigurationManager.AppSettings["Gemini_Settings_LanguageCode_PreventEditing"];
                if(s == null || !bool.TryParse(s, out var r)) return true;
                return !r;
            }
        }

        public bool AutoHideMainMenu
        {
            get { return _autoHideMainMenu; }
            set
            {
                if (value.Equals(_autoHideMainMenu)) return;
                _autoHideMainMenu = value;
                NotifyOfPropertyChange(() => AutoHideMainMenu);
            }
        }

        public string SettingsPageName => Resources.SettingsPageGeneral;

        public string SettingsPagePath => Resources.SettingsPathEnvironment;

        public bool IsVisible => true;

        [ImportingConstructor]
        public MainMenuSettingsViewModel(IThemeManager themeManager)
        {
            _themeManager = themeManager;
        }

        public void Load() {
            SelectedTheme = _themeManager.CurrentTheme;
            AutoHideMainMenu = Properties.Settings.Default.AutoHideMainMenu;
            SelectedLanguage = Properties.Settings.Default.LanguageCode;
        }

        public void ApplyChanges()
        {
            Properties.Settings.Default.ThemeName = SelectedTheme.GetType().Name;
            Properties.Settings.Default.AutoHideMainMenu = AutoHideMainMenu;
            Properties.Settings.Default.LanguageCode = SelectedLanguage;
            Properties.Settings.Default.Save();
        }
    }
}
