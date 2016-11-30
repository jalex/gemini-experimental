#region

using System;
using System.Windows.Input;
using Gemini.Framework.Commands;

#endregion

namespace Gemini.Framework.Menus
{
    public class MenuDefinition : MenuDefinitionBase
    {
        public MenuDefinition(MenuBarDefinition menuBar, int sortOrder, string text)
        {
            MenuBar = menuBar;
            SortOrder = sortOrder;
            Text = text;
        }

        public MenuBarDefinition MenuBar { get; }

        public override int SortOrder { get; }

        public override string Text { get; }

        public override Uri IconSource => null;

        public override KeyGesture KeyGesture => null;

        public override CommandDefinitionBase CommandDefinition => null;
    }
}