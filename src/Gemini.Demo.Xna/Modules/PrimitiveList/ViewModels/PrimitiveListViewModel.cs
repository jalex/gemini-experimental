#region

using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Gemini.Demo.Xna.Primitives;
using Gemini.Framework;
using Gemini.Framework.Services;
using Microsoft.Xna.Framework;

#endregion

namespace Gemini.Demo.Xna.Modules.PrimitiveList.ViewModels
{

    /// <summary>
    ///     Represents the view model of the primitive list.
    /// </summary>
    [Export(typeof(PrimitiveListViewModel))]
    public class PrimitiveListViewModel : Tool
    {
        private readonly List<PrimitiveWithColor> _primitives;

        /// <summary>
        ///     Returns the preferred <see cref="PaneLocation"/> of the tool.
        /// </summary>
        public override PaneLocation PreferredLocation => PaneLocation.Right;

        /// <summary>
        ///     Returns a list of <see cref="PrimitiveWithColor"/>.
        /// </summary>
        public IList<PrimitiveWithColor> Primitives => _primitives;

        /// <summary>
        ///     Creates a new <see cref="PrimitiveListViewModel"/>.
        /// </summary>
        public PrimitiveListViewModel()
        {
            DisplayName = "Primitive List";

            _primitives = new List<PrimitiveWithColor>(
                new[]
                    {
                        Color.Blue, Color.Red, Color.Yellow, Color.Green, Color.Gold, Color.Fuchsia, Color.Black,
                        Color.SlateBlue
                    }
                    .Select(x => new PrimitiveWithColor
                    {
                        Primitive = new CubePrimitive(),
                        Color = x
                    }));
        }
    }
}
