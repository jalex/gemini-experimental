#region

using System;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace Gemini.Modules.Xna.Controls
{
    /// <summary>
    ///     Provides data for the Draw event.
    /// </summary>
    public sealed class DrawEventArgs : EventArgs
    {
        private readonly DrawingSurface _drawingSurface;

        public DrawEventArgs(DrawingSurface drawingSurface)
        {
            _drawingSurface = drawingSurface;
        }

        public GraphicsDevice GraphicsDevice => _drawingSurface.GraphicsDevice;

        public void InvalidateSurface()
        {
            _drawingSurface.Invalidate();
        }
    }
}