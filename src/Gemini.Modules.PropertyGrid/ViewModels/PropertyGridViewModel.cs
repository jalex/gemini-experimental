﻿#region

using System;
using System.ComponentModel.Composition;
using Gemini.Framework;
using Gemini.Framework.Commands;
using Gemini.Framework.Services;
using Gemini.Modules.PropertyGrid.Properties;
using Gemini.Modules.UndoRedo.Commands;

#endregion

namespace Gemini.Modules.PropertyGrid.ViewModels
{
    [Export(typeof(IPropertyGrid))]
    public class PropertyGridViewModel : Tool, IPropertyGrid, ICommandRerouter
    {
        private readonly IShell _shell;

        private object _selectedObject;

        [ImportingConstructor]
        public PropertyGridViewModel(IShell shell)
        {
            _shell = shell;
            DisplayName = Resources.PropertyGridViewModel_PropertyGridViewModel_Properties;
        }

        object ICommandRerouter.GetHandler(CommandDefinitionBase commandDefinition)
        {
            if (commandDefinition is UndoCommandDefinition ||
                commandDefinition is RedoCommandDefinition)
                return _shell.ActiveItem;

            return null;
        }

        public override PaneLocation PreferredLocation => PaneLocation.Right;

        public override Uri IconSource
            => new Uri("pack://application:,,,/Gemini.Modules.PropertyGrid;component/Resources/Icons/Properties.png");

        public object SelectedObject
        {
            get { return _selectedObject; }
            set
            {
                _selectedObject = value;
                NotifyOfPropertyChange(() => SelectedObject);
            }
        }
    }
}