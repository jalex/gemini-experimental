#region

using Gemini.Framework.Services;

#endregion

namespace Gemini.Framework
{

    /// <summary>
    ///     Represents a panel which provides tools and utilities for manipulating the
    ///     contents of an associated <see cref="IDocument"/> or global application data.
    /// </summary>
    public interface ITool : ILayoutPanel
    {

        /// <summary>
        ///     Returns the preferred <see cref="PaneLocation"/> of the tool.
        /// </summary>
        PaneLocation PreferredLocation { get; }

        /// <summary>
        ///     Returns the preferred width of the tool.
        /// </summary>
        double PreferredWidth { get; }

        /// <summary>
        ///     Returns the preferred height of the tool.
        /// </summary>
        double PreferredHeight { get; }

        /// <summary>
        ///     Gets or sets whether the tool is currently visible.
        /// </summary>
        bool IsVisible { get; set; }
    }
}
