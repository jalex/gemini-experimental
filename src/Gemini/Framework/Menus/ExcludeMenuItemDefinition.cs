﻿namespace Gemini.Framework.Menus
{
    public class ExcludeMenuItemDefinition
    {
        public MenuItemDefinition MenuItemDefinitionToExclude { get; }

        public ExcludeMenuItemDefinition(MenuItemDefinition menuItemDefinition)
        {
            MenuItemDefinitionToExclude = menuItemDefinition;
        }
    }
}
