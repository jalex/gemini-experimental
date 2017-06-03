#region

using System.Windows;

#endregion

namespace Gemini.Modules.GraphEditor.Controls
{
    public class ConnectionDragCompletedEventArgs : ConnectionDragEventArgs
    {
        public object Connection { get; }

        internal ConnectionDragCompletedEventArgs(RoutedEvent routedEvent, object source,
            ElementItem elementItem, object connection, ConnectorItem sourceConnectorItem)
            : base(routedEvent, source, elementItem, sourceConnectorItem)
        {
            Connection = connection;
        }
    }
}
