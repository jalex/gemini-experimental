#region

using System;
using System.Windows;

#endregion

namespace Gemini.Modules.Toolbox.Models
{
    public class ToolboxItem
    {
        public Type DocumentType { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public Uri IconSource { get; set; }
        public DragDropEffects AllowedEffects { get; set; }
        public Type ItemType { get; set; }
    }
}
