#region

using System.Windows;

#endregion

namespace Gemini.Modules.GraphEditor.Controls
{
    internal class ConnectorItemDraggingEventArgs : RoutedEventArgs
    {
        public ConnectorItemDraggingEventArgs(RoutedEvent routedEvent, object source, double horizontalChange,
            double verticalChange) :
            base(routedEvent, source)
        {
            HorizontalChange = horizontalChange;
            VerticalChange = verticalChange;
        }

        public double HorizontalChange { get; }

        public double VerticalChange { get; }
    }

    internal delegate void ConnectorItemDraggingEventHandler(object sender, ConnectorItemDraggingEventArgs e);
}