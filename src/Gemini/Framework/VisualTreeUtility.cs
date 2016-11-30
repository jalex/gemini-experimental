﻿#region

using System.Windows;
using System.Windows.Media;

#endregion

namespace Gemini.Framework
{
    public static class VisualTreeUtility
    {
        public static T FindParent<T>(DependencyObject child)
            where T : DependencyObject
        {
            var parentObject = VisualTreeHelper.GetParent(child);

            if (parentObject == null)
                return null;

            var parent = parentObject as T;
            if (parent != null)
                return parent;

            return FindParent<T>(parentObject);
        }
    }
}