#region

using System.Windows;

#endregion

namespace Gemini.Modules.GraphEditor.Controls
{
    internal class ConnectorItemDraggingEventArgs : RoutedEventArgs
    {
        public double HorizontalChange { get; }

        public double VerticalChange { get; }

        public ConnectorItemDraggingEventArgs(RoutedEvent routedEvent, object source, double horizontalChange,
            double verticalChange) :
            base(routedEvent, source)
        {
            HorizontalChange = horizontalChange;
            VerticalChange = verticalChange;
        }
    }
}