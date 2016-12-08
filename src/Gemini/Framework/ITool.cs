#region

using Gemini.Framework.Services;

#endregion

namespace Gemini.Framework
{
    public interface ITool : ILayoutPanel
    {
        PaneLocation PreferredLocation { get; }
        double PreferredWidth { get; }
        double PreferredHeight { get; }

        bool IsVisible { get; set; }
    }
}
