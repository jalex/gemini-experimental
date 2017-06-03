#region

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace Gemini.Demo.MonoGame.Primitives
{
    /// <summary>
    ///     Custom vertex type for vertices that have just a
    ///     position and a normal, without any texture coordinates.
    /// </summary>
    public struct VertexPositionNormal : IVertexType
    {
        /// <summary>
        ///     Specifies the position of the vertex as <see cref="Vector3" />.
        /// </summary>
        public Vector3 Position;

        /// <summary>
        ///     Specifies the normal of the vertex as <see cref="Vector3" />.
        /// </summary>
        public Vector3 Normal;

        /// <summary>
        ///     Creates a new <see cref="VertexPositionNormal" />.
        /// </summary>
        public VertexPositionNormal(Vector3 position, Vector3 normal)
        {
            Position = position;
            Normal = normal;
        }

        /// <summary>
        ///     A VertexDeclaration object, which contains information about the vertex
        ///     elements contained within this struct.
        /// </summary>
        public static readonly VertexDeclaration VertexDeclaration = new VertexDeclaration
        (
            new VertexElement(0, VertexElementFormat.Vector3, VertexElementUsage.Position, 0),
            new VertexElement(12, VertexElementFormat.Vector3, VertexElementUsage.Normal, 0)
        );

        VertexDeclaration IVertexType.VertexDeclaration => VertexDeclaration;
    }
}
