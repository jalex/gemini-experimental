#region

using System;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace Gemini.Modules.Xna.Controls
{
    /// <summary>
    ///     Arguments used for GraphicsDevice related events.
    /// </summary>
    public class GraphicsDeviceEventArgs : EventArgs
    {
        /// <summary>
        ///     Initializes a new GraphicsDeviceEventArgs.
        /// </summary>
        /// <param name="device">The GraphicsDevice associated with the event.</param>
        public GraphicsDeviceEventArgs(GraphicsDevice device)
        {
            GraphicsDevice = device;
        }

        /// <summary>
        ///     Gets the GraphicsDevice.
        /// </summary>
        public GraphicsDevice GraphicsDevice { get; private set; }
    }
}