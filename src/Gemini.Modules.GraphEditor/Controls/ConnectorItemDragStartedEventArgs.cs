#region

using System.Windows;

#endregion

namespace Gemini.Modules.GraphEditor.Controls
{
    internal class ConnectorItemDragStartedEventArgs : RoutedEventArgs
    {
        public bool Cancel { get; set; }

        internal ConnectorItemDragStartedEventArgs(RoutedEvent routedEvent, object source)
            : base(routedEvent, source)
        {
        }
    }
}