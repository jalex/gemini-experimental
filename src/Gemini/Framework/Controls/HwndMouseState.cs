#region

using System.Windows;
using System.Windows.Input;

#endregion

namespace Gemini.Framework.Controls
{
    public class HwndMouseState
    {
        /// <summary>
        ///     The current state of the left mouse button.
        /// </summary>
        public MouseButtonState LeftButton;

        /// <summary>
        ///     The current state of the middle mouse button.
        /// </summary>
        public MouseButtonState MiddleButton;

        /// <summary>
        ///     The current state of the right mouse button.
        /// </summary>
        public MouseButtonState RightButton;

        /// <summary>
        ///     The current position of the mouse in screen coordinates.
        /// </summary>
        public Point ScreenPosition;

        /// <summary>
        ///     The current state of the first extra mouse button.
        /// </summary>
        public MouseButtonState X1Button;

        /// <summary>
        ///     The current state of the second extra mouse button.
        /// </summary>
        public MouseButtonState X2Button;
    }
}
