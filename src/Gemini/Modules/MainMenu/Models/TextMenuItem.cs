#region

using System;
using System.Globalization;
using System.Windows.Input;
using Gemini.Framework.Menus;

#endregion

namespace Gemini.Modules.MainMenu.Models
{
    public class TextMenuItem : StandardMenuItem
    {
        private readonly MenuDefinitionBase _menuDefinition;

        public override string Text => _menuDefinition.Text;

        public override Uri IconSource => _menuDefinition.IconSource;

        public override string InputGestureText => _menuDefinition.KeyGesture == null
            ? string.Empty
            : _menuDefinition.KeyGesture.GetDisplayStringForCulture(CultureInfo.CurrentUICulture);

        public override ICommand Command => null;

        public override bool IsChecked => false;

        public override bool IsVisible => true;

        public TextMenuItem(MenuDefinitionBase menuDefinition)
        {
            _menuDefinition = menuDefinition;
        }
    }
}