#region

using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using Size = System.Drawing.Size;

#endregion

namespace Gemini.Modules.Inspector.Util
{
    public class ScreenShotUtility
    {
        public static Bitmap Take()
        {
            var screenX = (int) SystemParameters.VirtualScreenWidth;
            var screenY = (int) SystemParameters.VirtualScreenHeight;

            var ret = new Bitmap(screenX, screenY, PixelFormat.Format32bppRgb);

            using (var graphics = Graphics.FromImage(ret))
            {
                graphics.CopyFromScreen((int) SystemParameters.VirtualScreenLeft,
                    (int) SystemParameters.VirtualScreenTop, 0, 0,
                    new Size((int) SystemParameters.VirtualScreenWidth, (int) SystemParameters.VirtualScreenHeight),
                    CopyPixelOperation.SourceCopy);
            }

            return ret;
        }
    }
}
