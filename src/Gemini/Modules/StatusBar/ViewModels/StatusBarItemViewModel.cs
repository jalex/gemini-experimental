#region

using System.Windows;
using Caliburn.Micro;

#endregion

namespace Gemini.Modules.StatusBar.ViewModels
{
    public class StatusBarItemViewModel : PropertyChangedBase
    {
        private int _index;

        private string _message;

        public int Index
        {
            get { return _index; }
            internal set
            {
                _index = value;
                NotifyOfPropertyChange(() => Index);
            }
        }

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                NotifyOfPropertyChange(() => Message);
            }
        }

        public GridLength Width { get; }

        public StatusBarItemViewModel(string message, GridLength width)
        {
            _message = message;
            Width = width;
        }
    }
}
