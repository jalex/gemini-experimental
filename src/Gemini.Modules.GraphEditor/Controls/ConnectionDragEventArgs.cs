#region

using System.Windows;

#endregion

namespace Gemini.Modules.GraphEditor.Controls
{
    public abstract class ConnectionDragEventArgs : RoutedEventArgs
    {
        protected ConnectionDragEventArgs(RoutedEvent routedEvent, object source,
            ElementItem elementItem, ConnectorItem sourceConnectorItem)
            : base(routedEvent, source)
        {
            ElementItem = elementItem;
            SourceConnector = sourceConnectorItem;
        }

        public ElementItem ElementItem { get; }

        public ConnectorItem SourceConnector { get; }
    }
}