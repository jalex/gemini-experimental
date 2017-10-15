using System;

namespace Gemini.Framework.Menus {

    public class MenuDefinitionEx: MenuDefinition, IMenuDefinitionEx {

        public virtual bool IsVisible => IsVisibleFunc == null || IsVisibleFunc();

        public Func<bool> IsVisibleFunc { get; set; }

        public MenuDefinitionEx(MenuBarDefinition menuBar, int sortOrder, string text) : base(menuBar, sortOrder, text) {
        }
    }
}
