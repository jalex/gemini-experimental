#region

using System.Windows;

#endregion

namespace Gemini.Modules.GraphEditor.Controls
{
    public class ConnectionDragStartedEventArgs : ConnectionDragEventArgs
    {
        public ConnectionDragStartedEventArgs(RoutedEvent routedEvent, object source,
            ElementItem elementItem, ConnectorItem connectorItem)
            : base(routedEvent, source, elementItem, connectorItem)
        {
        }

        public object Connection { get; set; }
    }

    public delegate void ConnectionDragStartedEventHandler(object sender, ConnectionDragStartedEventArgs e);
}