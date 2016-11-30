#region

using System.Windows;

#endregion

namespace Gemini.Modules.GraphEditor.Controls
{
    public class ConnectionDraggingEventArgs : ConnectionDragEventArgs
    {
        internal ConnectionDraggingEventArgs(RoutedEvent routedEvent, object source,
            ElementItem elementItem, object connection, ConnectorItem connectorItem)
            : base(routedEvent, source, elementItem, connectorItem)
        {
            Connection = connection;
        }

        public object Connection { get; }
    }

    public delegate void ConnectionDraggingEventHandler(object sender, ConnectionDraggingEventArgs e);
}