#region

using System.Windows;

#endregion

namespace Gemini.Modules.GraphEditor.Controls
{
    public class ConnectionDragStartedEventArgs : ConnectionDragEventArgs
    {
        public object Connection { get; set; }

        public ConnectionDragStartedEventArgs(RoutedEvent routedEvent, object source,
            ElementItem elementItem, ConnectorItem connectorItem)
            : base(routedEvent, source, elementItem, connectorItem)
        {
        }
    }
}
