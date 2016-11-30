#region

using System;
using System.Windows;

#endregion

namespace Gemini.Modules.Inspector.Controls
{
    public class InspectorGrid
    {
        private static GridLength _propertyNameColumnWidth = new GridLength(1, GridUnitType.Star);


        private static GridLength _propertyValueColumnWidth = new GridLength(1.5, GridUnitType.Star);

        public static GridLength PropertyNameColumnWidth
        {
            get { return _propertyNameColumnWidth; }
            set
            {
                _propertyNameColumnWidth = value;
                var handler = PropertyNameColumnWidthChanged;
                if (handler != null)
                    handler(null, EventArgs.Empty);
            }
        }

        public static GridLength PropertyValueColumnWidth
        {
            get { return _propertyValueColumnWidth; }
            set
            {
                _propertyValueColumnWidth = value;
                var handler = PropertyValueColumnWidthChanged;
                if (handler != null)
                    handler(null, EventArgs.Empty);
            }
        }

        public static event EventHandler PropertyNameColumnWidthChanged;
        public static event EventHandler PropertyValueColumnWidthChanged;
    }
}