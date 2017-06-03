#region

#region

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;
using System.Windows.Media;

#endregion

// ReSharper disable UnusedVariable

#endregion

namespace Gemini.Framework.Behaviors
{
    // http://stackoverflow.com/a/20636049/208817
    /// <summary>
    ///     Represents a <see cref="Behavior{T}" /> for the <see cref="TreeView" /> which allows
    ///     binding to the current <see cref="TreeView.SelectedItem" />.
    /// </summary>
    public class BindableTreeViewSelectedItemBehavior : Behavior<TreeView>
    {
        /// <summary>
        ///     Specifies the <see cref="DependencyProperty" /> for the <see cref="SelectedItem" />.
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register(
            nameof(SelectedItem), typeof(object), typeof(BindableTreeViewSelectedItemBehavior),
            new UIPropertyMetadata(null, OnSelectedItemChanged));

        /// <summary>
        ///     Gets or sets the currently selected item.
        /// </summary>
        public object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        private static void OnSelectedItemChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Action<TreeViewItem> selectTreeViewItem = tvi2 =>
            {
                if (tvi2 == null)
                    return;
                tvi2.IsSelected = true;
                tvi2.Focus();
            };

            var tvi = e.NewValue as TreeViewItem;

            if (tvi == null)
            {
                var tree = ((BindableTreeViewSelectedItemBehavior) sender).AssociatedObject;
                if (tree == null)
                    return;

                if (!tree.IsLoaded)
                {
                    RoutedEventHandler handler = null;
                    handler = (sender2, e2) =>
                    {
                        tvi = GetTreeViewItem(tree, e.NewValue);
                        selectTreeViewItem(tvi);
                        tree.Loaded -= handler;
                    };
                    tree.Loaded += handler;

                    return;
                }
                tvi = GetTreeViewItem(tree, e.NewValue);
            }

            selectTreeViewItem(tvi);
        }


        private void OnTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            SelectedItem = e.NewValue;
        }

        private static TreeViewItem GetTreeViewItem(ItemsControl container, object item)
        {
            if (container == null)
                return null;

            if (container.DataContext == item)
                return container as TreeViewItem;

            // Expand the current container
            var viewItem = container as TreeViewItem;
            if (viewItem != null && !viewItem.IsExpanded)
                viewItem.SetValue(TreeViewItem.IsExpandedProperty, true);

            // Try to generate the ItemsPresenter and the ItemsPanel.
            // by calling ApplyTemplate.  Note that in the
            // virtualizing case even if the item is marked
            // expanded we still need to do this step in order to
            // regenerate the visuals because they may have been virtualized away.

            container.ApplyTemplate();
            var itemsPresenter =
                (ItemsPresenter) container.Template.FindName("ItemsHost", container);
            if (itemsPresenter != null)
            {
                itemsPresenter.ApplyTemplate();
            }
            else
            {
                // The Tree template has not named the ItemsPresenter,
                // so walk the descendents and find the child.
                itemsPresenter = container.FindChild<ItemsPresenter>();
                if (itemsPresenter == null)
                {
                    container.UpdateLayout();
                    itemsPresenter = container.FindChild<ItemsPresenter>();
                }
            }

            var itemsHostPanel = (Panel) VisualTreeHelper.GetChild(itemsPresenter, 0);

            // Ensure that the generator for this panel has been created.
#pragma warning disable 168
            var children = itemsHostPanel.Children;
#pragma warning restore 168

            for (int i = 0, count = container.Items.Count; i < count; i++)
            {
                var subContainer = (TreeViewItem) container.ItemContainerGenerator.ContainerFromIndex(i);
                if (subContainer == null)
                    continue;

                subContainer.BringIntoView();

                // Search the next level for the object.
                var resultContainer = GetTreeViewItem(subContainer, item);
                if (resultContainer != null)
                    return resultContainer;
            }

            return null;
        }


        /// <summary>
        ///     Called after the behavior is attached to an AssociatedObject.
        /// </summary>
        /// <remarks>Override this to hook up functionality to the AssociatedObject.</remarks>
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.SelectedItemChanged += OnTreeViewSelectedItemChanged;
        }

        /// <summary>
        ///     Called when the behavior is being detached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        /// <remarks>Override this to unhook functionality from the AssociatedObject.</remarks>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (AssociatedObject != null)
                AssociatedObject.SelectedItemChanged -= OnTreeViewSelectedItemChanged;
        }
    }
}
