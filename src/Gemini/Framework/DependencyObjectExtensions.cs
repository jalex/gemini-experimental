#region

using System.Windows;
using System.Windows.Media;

#endregion

namespace Gemini.Framework
{
    /// <summary>
    ///     Extensions for <see cref="DependencyObject" />.
    /// </summary>
    public static class DependencyObjectExtensions
    {
        /// <summary>
        ///     Search for a child element of a certain type in the visual tree.
        /// </summary>
        /// <typeparam name="T">The type of element to find.</typeparam>
        /// <param name="parent">The parent element.</param>
        /// <returns>The <see cref="DependencyObject" /> if available, otherwise null.</returns>
        public static T FindChild<T>(this DependencyObject parent) where T : DependencyObject
        {
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child == null) continue;
                var correctlyTyped = child as T;
                if (correctlyTyped != null)
                    return correctlyTyped;

                var descendent = FindChild<T>(child);
                if (descendent != null)
                    return descendent;
            }

            return null;
        }

        /// <summary>
        ///     Searches for a parent element of a certain type in the visual tree.
        /// </summary>
        /// <typeparam name="T">The type of element to find.</typeparam>
        /// <param name="child">The child element.</param>
        /// <returns>The <see cref="DependencyObject" /> if available, otherwise null.</returns>
        public static T FindParent<T>(this DependencyObject child) where T : DependencyObject
        {
            while (true)
            {
                var parentObject = VisualTreeHelper.GetParent(child);

                if (parentObject == null)
                    return null;

                var parent = parentObject as T;
                if (parent != null)
                    return parent;

                child = parentObject;
            }
        }
    }
}
