#region

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Gemini.Properties;

#endregion

namespace Gemini.Framework.Themes
{
    [Export(typeof(ITheme))]
    public class DarkTheme : ITheme
    {
        public virtual string Name => Resources.ThemeDarkName;

        public virtual IEnumerable<Uri> ApplicationResources
        {
            get
            {
                yield return
                    new Uri("pack://application:,,,/Xceed.Wpf.AvalonDock.Themes.VS2013;component/DarkTheme.xaml");
                yield return new Uri("pack://application:,,,/Gemini;component/Themes/VS2013/DarkTheme.xaml");
            }
        }

        public virtual IEnumerable<Uri> MainWindowResources
        {
            get { yield break; }
        }
    }
}
