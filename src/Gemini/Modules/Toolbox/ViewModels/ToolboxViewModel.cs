﻿#region

using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using Caliburn.Micro;
using Gemini.Framework;
using Gemini.Framework.Services;
using Gemini.Modules.Toolbox.Services;
using Gemini.Properties;

#endregion

namespace Gemini.Modules.Toolbox.ViewModels
{
    [Export(typeof(IToolbox))]
    public class ToolboxViewModel : Tool, IToolbox
    {
        private readonly BindableCollection<ToolboxItemViewModel> _items;
        private readonly IToolboxService _toolboxService;

        public IObservableCollection<ToolboxItemViewModel> Items => _items;

        public override PaneLocation PreferredLocation => PaneLocation.Left;

        [ImportingConstructor]
        public ToolboxViewModel(IShell shell, IToolboxService toolboxService)
        {
            DisplayName = Resources.ToolboxDisplayName;

            _items = new BindableCollection<ToolboxItemViewModel>();

            var groupedItems = CollectionViewSource.GetDefaultView(_items);
            groupedItems.GroupDescriptions.Add(new PropertyGroupDescription(nameof(ToolboxItemViewModel.Category)));

            _toolboxService = toolboxService;

            if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
                return;

            shell.ActiveDocumentChanged += (sender, e) => RefreshToolboxItems(shell);
            RefreshToolboxItems(shell);
        }

        private void RefreshToolboxItems(IShell shell)
        {
            _items.Clear();

            if (shell.SelectedDocument == null)
                return;

            _items.AddRange(_toolboxService.GetToolboxItems(shell.SelectedDocument.GetType())
                .Select(x => new ToolboxItemViewModel(x)));
        }
    }
}