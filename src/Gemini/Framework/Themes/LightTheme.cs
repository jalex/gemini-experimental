#region

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Gemini.Properties;

#endregion

namespace Gemini.Framework.Themes
{
    [Export(typeof(ITheme))]
    public class LightTheme : ITheme
    {
        public virtual string Name => Resources.ThemeLightName;

        public virtual IEnumerable<Uri> ApplicationResources
        {
            get
            {
                yield return
                    new Uri("pack://application:,,,/Xceed.Wpf.AvalonDock.Themes.VS2013;component/LightTheme.xaml");
                yield return new Uri("pack://application:,,,/Gemini;component/Themes/VS2013/LightTheme.xaml");
            }
        }

        public virtual IEnumerable<Uri> MainWindowResources
        {
            get { yield break; }
        }
    }
}
