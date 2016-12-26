#region

using System;
using System.Windows;

#endregion

namespace Gemini.Modules.Toolbox
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ToolboxItemAttribute : Attribute
    {
        public Type DocumentType { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string IconSource { get; set; }
        public DragDropEffects AllowedEffects { get; set; }

        public ToolboxItemAttribute(Type documentType, string name, string category, string iconSource = null, DragDropEffects allowedEffects = DragDropEffects.Move)
        {
            DocumentType = documentType;
            Name = name;
            Category = category;
            IconSource = iconSource;
            AllowedEffects = allowedEffects;
        }
    }
}