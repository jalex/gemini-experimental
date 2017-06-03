#region

using System;
using Gemini.Modules.Toolbox.Models;

#endregion

namespace Gemini.Modules.Toolbox.ViewModels
{
    public class ToolboxItemViewModel
    {
        public ToolboxItem Model { get; }

        public string Name => Model.Name;

        public virtual string Category => Model.Category;

        public virtual Uri IconSource => Model.IconSource;

        public ToolboxItemViewModel(ToolboxItem model)
        {
            Model = model;
        }
    }
}
