#region

using System.Windows;

#endregion

namespace Gemini.Modules.GraphEditor.Controls
{
    internal class ConnectorItemDragStartedEventArgs : RoutedEventArgs
    {
        internal ConnectorItemDragStartedEventArgs(RoutedEvent routedEvent, object source)
            : base(routedEvent, source)
        {
        }

        public bool Cancel { get; set; }
    }

    internal delegate void ConnectorItemDragStartedEventHandler(object sender, ConnectorItemDragStartedEventArgs e);
}