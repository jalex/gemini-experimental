#region

using System;
using System.Collections.Generic;

#endregion

namespace Gemini.Framework.Themes
{
    public interface IThemeManager
    {
        List<ITheme> Themes { get; }
        ITheme CurrentTheme { get; }
        event EventHandler CurrentThemeChanged;

        bool SetCurrentTheme(string name);
    }
}