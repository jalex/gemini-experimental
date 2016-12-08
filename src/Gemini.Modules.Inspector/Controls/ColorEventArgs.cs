#region

using System;
using System.Windows.Media;

#endregion

namespace Gemini.Modules.Inspector.Controls
{
    public class ColorEventArgs : EventArgs
    {
        public Color Color { get; private set; }

        public ColorEventArgs(Color color)
        {
            Color = color;
        }
    }
}