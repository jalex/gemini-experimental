﻿namespace Gemini.Framework.Menus
{
    public class ExcludeMenuItemGroupDefinition
    {
        public MenuItemGroupDefinition MenuItemGroupDefinitionToExclude { get; }

        public ExcludeMenuItemGroupDefinition(MenuItemGroupDefinition menuItemGroupDefinition)
        {
            MenuItemGroupDefinitionToExclude = menuItemGroupDefinition;
        }
    }
}
