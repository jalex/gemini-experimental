#region

using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

#endregion

namespace Gemini.Modules.Inspector.MonoGame.Converters
{
    public class XnaColorToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert((Microsoft.Xna.Framework.Color) value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert((Color) value);
        }

        public static Color Convert(Microsoft.Xna.Framework.Color c)
        {
            return Color.FromArgb(c.A, c.R, c.G, c.B);
        }

        public static Microsoft.Xna.Framework.Color Convert(Color c)
        {
            return Microsoft.Xna.Framework.Color.FromNonPremultiplied(c.R, c.G, c.B, c.A);
        }
    }
}