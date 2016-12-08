#region

using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

#endregion

namespace Gemini.Demo.Modules.FilterDesigner.ViewModels
{
    public class InputConnectorViewModel : ConnectorViewModel
    {
        private ConnectionViewModel _connection;

        public override ConnectorDirection ConnectorDirection => ConnectorDirection.Input;

        public ConnectionViewModel Connection
        {
            get { return _connection; }
            set
            {
                if (_connection != null)
                    _connection.From.Element.OutputChanged -= OnSourceElementOutputChanged;
                _connection = value;
                if (_connection != null)
                    _connection.From.Element.OutputChanged += OnSourceElementOutputChanged;
                RaiseSourceChanged();
                NotifyOfPropertyChange(() => Connection);
            }
        }

        public BitmapSource Value => Connection?.From?.Value;

        public InputConnectorViewModel(ElementViewModel element, string name, Color color)
            : base(element, name, color)
        {
        }

        public event EventHandler SourceChanged;

        private void OnSourceElementOutputChanged(object sender, EventArgs e)
        {
            RaiseSourceChanged();
        }

        private void RaiseSourceChanged()
        {
            var handler = SourceChanged;
            handler?.Invoke(this, EventArgs.Empty);
        }
    }
}