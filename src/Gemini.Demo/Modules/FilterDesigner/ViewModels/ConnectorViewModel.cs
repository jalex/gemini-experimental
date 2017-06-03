#region

using System;
using System.Windows;
using System.Windows.Media;
using Caliburn.Micro;

#endregion

namespace Gemini.Demo.Modules.FilterDesigner.ViewModels
{
    public abstract class ConnectorViewModel : PropertyChangedBase
    {
        private Point _position;

        public ElementViewModel Element { get; }

        public string Name { get; }

        public Color Color { get; } = Colors.Black;

        public Point Position
        {
            get { return _position; }
            set
            {
                _position = value;
                NotifyOfPropertyChange(() => Position);
                RaisePositionChanged();
            }
        }

        public abstract ConnectorDirection ConnectorDirection { get; }

        protected ConnectorViewModel(ElementViewModel element, string name, Color color)
        {
            Element = element;
            Name = name;
            Color = color;
        }

        public event EventHandler PositionChanged;

        private void RaisePositionChanged()
        {
            var handler = PositionChanged;
            handler?.Invoke(this, EventArgs.Empty);
        }
    }
}
