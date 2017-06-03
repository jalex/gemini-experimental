#region

using System;
using System.Globalization;
using System.Windows.Data;
using Gemini.Properties;

#endregion

namespace Gemini.Modules.MainMenu.Converters
{
    public class CultureInfoNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            if (string.Empty.Equals(value))
            {
                if (Resources.LanguageSystem.Equals("System"))
                    return Resources.LanguageSystem;

                return
                    $"{Resources.LanguageSystem} ({Resources.ResourceManager.GetString("LanguageSystem", CultureInfo.InvariantCulture)})";
            }

            var cn = value as string;
            var ci = CultureInfo.GetCultureInfo(cn);

            if (Equals(ci.NativeName, ci.EnglishName))
                return ci.NativeName;

            return $"{ci.NativeName} ({ci.EnglishName})";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
