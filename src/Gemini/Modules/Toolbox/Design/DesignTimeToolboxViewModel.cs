#region

using Gemini.Modules.Toolbox.Models;
using Gemini.Modules.Toolbox.ViewModels;

#endregion

namespace Gemini.Modules.Toolbox.Design
{
    public class DesignTimeToolboxViewModel : ToolboxViewModel
    {
        public DesignTimeToolboxViewModel()
            : base(null, null)
        {
            Items.Add(new ToolboxItemViewModel(new ToolboxItem {Name = "Foo", Category = "General"}));
            Items.Add(new ToolboxItemViewModel(new ToolboxItem {Name = "Bar", Category = "General"}));
            Items.Add(new ToolboxItemViewModel(new ToolboxItem {Name = "Baz", Category = "Misc"}));
        }
    }
}