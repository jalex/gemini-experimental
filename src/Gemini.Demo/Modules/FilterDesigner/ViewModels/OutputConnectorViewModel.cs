#region

using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Caliburn.Micro;

#endregion

namespace Gemini.Demo.Modules.FilterDesigner.ViewModels
{
    public class OutputConnectorViewModel : ConnectorViewModel
    {
        private readonly BindableCollection<ConnectionViewModel> _connections;
        private readonly Func<BitmapSource> _valueCallback;

        public OutputConnectorViewModel(ElementViewModel element, string name, Color color,
            Func<BitmapSource> valueCallback)
            : base(element, name, color)
        {
            _connections = new BindableCollection<ConnectionViewModel>();
            _valueCallback = valueCallback;
        }

        public override ConnectorDirection ConnectorDirection => ConnectorDirection.Output;
        public IObservableCollection<ConnectionViewModel> Connections => _connections;

        public BitmapSource Value => _valueCallback();
    }
}