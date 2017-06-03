#region

using Caliburn.Micro;

#endregion

namespace Gemini.Modules.ToolBars.Models
{
    public class ToolBarItemBase : PropertyChangedBase
    {
        public static ToolBarItemBase Separator => new ToolBarItemSeparator();

        public virtual string Name => "-";
    }
}
