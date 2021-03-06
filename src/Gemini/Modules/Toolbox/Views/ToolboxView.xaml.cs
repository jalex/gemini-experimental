﻿#region

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Gemini.Framework;
using Gemini.Modules.Toolbox.ViewModels;

#endregion

namespace Gemini.Modules.Toolbox.Views
{
    /// <summary>
    ///     Interaction logic for ToolboxView.xaml
    /// </summary>
    public partial class ToolboxView
    {
        private bool _draggingItem;
        private Point _mouseStartPosition;

        public ToolboxView()
        {
            InitializeComponent();
        }

        private void OnListBoxPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var listBoxItem = ((DependencyObject) e.OriginalSource).FindParent<ListBoxItem>();
            _draggingItem = listBoxItem != null;

            _mouseStartPosition = e.GetPosition(ListBox);
        }

        private void OnListBoxMouseMove(object sender, MouseEventArgs e)
        {
            if (!_draggingItem)
                return;

            // Get the current mouse position
            var mousePosition = e.GetPosition(null);
            var diff = _mouseStartPosition - mousePosition;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                 Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                var listBoxItem = ((DependencyObject) e.OriginalSource).FindParent<ListBoxItem>();

                if (listBoxItem == null)
                    return;

                var itemViewModel =
                    (ToolboxItemViewModel) ListBox.ItemContainerGenerator.ItemFromContainer(listBoxItem);

                var dragData = new DataObject(ToolboxDragDrop.DataFormat, itemViewModel.Model);
                DragDrop.DoDragDrop(listBoxItem, dragData, itemViewModel.Model.AllowedEffects);
            }
        }
    }
}
