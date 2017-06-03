#region

using System.Windows;

#endregion

namespace Gemini.Modules.GraphEditor.Controls
{
    public class ConnectionDraggingEventArgs : ConnectionDragEventArgs
    {
        public object Connection { get; }

        internal ConnectionDraggingEventArgs(RoutedEvent routedEvent, object source,
            ElementItem elementItem, object connection, ConnectorItem connectorItem)
            : base(routedEvent, source, elementItem, connectorItem)
        {
            Connection = connection;
        }
    }
}
