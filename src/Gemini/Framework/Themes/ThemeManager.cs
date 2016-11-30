#region

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Gemini.Framework.Services;
using Gemini.Properties;

#endregion

namespace Gemini.Framework.Themes
{
    [Export(typeof(IThemeManager))]
    public class ThemeManager : IThemeManager
    {
        private readonly SettingsPropertyChangedEventManager<Settings> _settingsEventManager =
            new SettingsPropertyChangedEventManager<Settings>(Settings.Default);

        private ResourceDictionary _applicationResourceDictionary;

        [ImportingConstructor]
        public ThemeManager([ImportMany] ITheme[] themes)
        {
            Themes = new List<ITheme>(themes);
            _settingsEventManager.AddListener(s => s.ThemeName, value => SetCurrentTheme(value));
        }

        public event EventHandler CurrentThemeChanged;

        public List<ITheme> Themes { get; }

        public ITheme CurrentTheme { get; private set; }

        public bool SetCurrentTheme(string name)
        {
            var theme = Themes.FirstOrDefault(x => x.GetType().Name == name);
            if (theme == null)
                return false;

            var mainWindow = Application.Current.MainWindow;
            if (mainWindow == null)
                return false;

            CurrentTheme = theme;

            if (_applicationResourceDictionary == null)
            {
                _applicationResourceDictionary = new ResourceDictionary();
                Application.Current.Resources.MergedDictionaries.Add(_applicationResourceDictionary);
            }
            _applicationResourceDictionary.BeginInit();
            _applicationResourceDictionary.MergedDictionaries.Clear();

            var windowResourceDictionary = mainWindow.Resources.MergedDictionaries[0];
            windowResourceDictionary.BeginInit();
            windowResourceDictionary.MergedDictionaries.Clear();

            foreach (var uri in theme.ApplicationResources)
                _applicationResourceDictionary.MergedDictionaries.Add(new ResourceDictionary
                {
                    Source = uri
                });

            foreach (var uri in theme.MainWindowResources)
                windowResourceDictionary.MergedDictionaries.Add(new ResourceDictionary
                {
                    Source = uri
                });

            _applicationResourceDictionary.EndInit();
            windowResourceDictionary.EndInit();

            RaiseCurrentThemeChanged(EventArgs.Empty);

            return true;
        }

        private void RaiseCurrentThemeChanged(EventArgs args)
        {
            var handler = CurrentThemeChanged;
            if (handler != null)
                handler(this, args);
        }
    }
}