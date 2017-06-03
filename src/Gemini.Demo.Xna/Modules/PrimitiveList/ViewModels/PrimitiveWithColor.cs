#region

using Gemini.Demo.Xna.Primitives;
using Microsoft.Xna.Framework;

#endregion

namespace Gemini.Demo.Xna.Modules.PrimitiveList.ViewModels
{
    public class PrimitiveWithColor
    {
        public GeometricPrimitive Primitive { get; set; }
        public Color Color { get; set; }
    }
}
