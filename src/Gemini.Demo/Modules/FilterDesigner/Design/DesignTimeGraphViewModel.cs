#region

using Gemini.Demo.Modules.FilterDesigner.ViewModels;

#endregion

namespace Gemini.Demo.Modules.FilterDesigner.Design
{
    /// <summary>
    ///     Represents a design time view model for the <see cref="GraphViewModel" />.
    /// </summary>
    public class DesignTimeGraphViewModel : GraphViewModel
    {
        /// <summary>
        ///     Creates a new <see cref="DesignTimeGraphViewModel" />.
        /// </summary>
        public DesignTimeGraphViewModel()
            : base(null)
        {
        }
    }
}
