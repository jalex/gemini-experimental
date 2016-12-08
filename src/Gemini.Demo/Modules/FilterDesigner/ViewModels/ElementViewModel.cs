#region

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Caliburn.Micro;

#endregion

namespace Gemini.Demo.Modules.FilterDesigner.ViewModels
{
    public abstract class ElementViewModel : PropertyChangedBase
    {
        public const double PreviewSize = 100;

        private readonly BindableCollection<InputConnectorViewModel> _inputConnectors;

        private bool _isSelected;

        private string _name;

        private OutputConnectorViewModel _outputConnector;

        private double _x;

        private double _y;

        [Browsable(false)]
        public double X
        {
            get { return _x; }
            set
            {
                _x = value;
                NotifyOfPropertyChange(() => X);
            }
        }

        [Browsable(false)]
        public double Y
        {
            get { return _y; }
            set
            {
                _y = value;
                NotifyOfPropertyChange(() => Y);
            }
        }

        [Browsable(false)]
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                NotifyOfPropertyChange(() => Name);
            }
        }

        [Browsable(false)]
        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                NotifyOfPropertyChange(() => IsSelected);
            }
        }

        public abstract BitmapSource PreviewImage { get; }
        public IList<InputConnectorViewModel> InputConnectors => _inputConnectors;

        public OutputConnectorViewModel OutputConnector
        {
            get { return _outputConnector; }
            set
            {
                _outputConnector = value;
                NotifyOfPropertyChange(() => OutputConnector);
            }
        }

        public IEnumerable<ConnectionViewModel> AttachedConnections
        {
            get
            {
                return _inputConnectors.Select(x => x.Connection)
                    .Union(_outputConnector.Connections)
                    .Where(x => x != null);
            }
        }

        protected ElementViewModel()
        {
            _inputConnectors = new BindableCollection<InputConnectorViewModel>();
            _name = GetType().Name;
        }

        public event EventHandler OutputChanged;

        protected void AddInputConnector(string name, Color color)
        {
            var inputConnector = new InputConnectorViewModel(this, name, color);
            inputConnector.SourceChanged += (sender, e) => OnInputConnectorConnectionChanged();
            _inputConnectors.Add(inputConnector);
        }

        protected void SetOutputConnector(string name, Color color, Func<BitmapSource> valueCallback)
        {
            OutputConnector = new OutputConnectorViewModel(this, name, color, valueCallback);
        }

        protected virtual void OnInputConnectorConnectionChanged()
        {
        }

        protected virtual void RaiseOutputChanged()
        {
            var handler = OutputChanged;
            handler?.Invoke(this, EventArgs.Empty);
        }
    }
}