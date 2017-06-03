#region

using System.Windows;

#endregion

namespace Gemini.Modules.GraphEditor.Controls
{
    internal class ConnectorItemDragCompletedEventArgs : RoutedEventArgs
    {
        public ConnectorItemDragCompletedEventArgs(RoutedEvent routedEvent, object source) :
            base(routedEvent, source)
        {
        }
    }
}
