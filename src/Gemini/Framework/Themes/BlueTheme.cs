#region

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Gemini.Properties;

#endregion

namespace Gemini.Framework.Themes
{
    [Export(typeof(ITheme))]
    public class BlueTheme : ITheme
    {
        public virtual string Name => Resources.ThemeBlueName;

        public virtual IEnumerable<Uri> ApplicationResources
        {
            get
            {
                yield return
                    new Uri("pack://application:,,,/Xceed.Wpf.AvalonDock.Themes.VS2013;component/BlueTheme.xaml");
                yield return new Uri("pack://application:,,,/Gemini;component/Themes/VS2013/BlueTheme.xaml");
            }
        }

        public virtual IEnumerable<Uri> MainWindowResources
        {
            get { yield break; }
        }
    }
}