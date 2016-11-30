#region

using System.ComponentModel.Composition;
using System.Windows;
using System.Windows.Media;
using Caliburn.Micro;
using Gemini.Framework.Commands;
using Gemini.Framework.Services;
using Gemini.Properties;

#endregion

namespace Gemini.Modules.MainWindow.ViewModels
{
    [Export(typeof(IMainWindow))]
    public class MainWindowViewModel : Conductor<IShell>, IMainWindow, IPartImportsSatisfiedNotification
    {
        private double _height = 800.0;

        private ImageSource _icon;

        private string _title = Resources.MainWindowDefaultTitle;

        private double _width = 1000.0;

        private WindowState _windowState = WindowState.Normal;

        public WindowState WindowState
        {
            get { return _windowState; }
            set
            {
                _windowState = value;
                NotifyOfPropertyChange(() => WindowState);
            }
        }

        public double Width
        {
            get { return _width; }
            set
            {
                _width = value;
                NotifyOfPropertyChange(() => Width);
            }
        }

        public double Height
        {
            get { return _height; }
            set
            {
                _height = value;
                NotifyOfPropertyChange(() => Height);
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyOfPropertyChange(() => Title);
            }
        }

        public ImageSource Icon
        {
            get { return _icon; }
            set
            {
                _icon = value;
                NotifyOfPropertyChange(() => Icon);
            }
        }

        public IShell Shell => _shell;

        void IPartImportsSatisfiedNotification.OnImportsSatisfied()
        {
            if (_icon == null)
                _icon = _resourceManager.GetBitmap("Resources/Icons/Gemini-32.png");
            ActivateItem(_shell);
        }

        protected override void OnViewLoaded(object view)
        {
            _commandKeyGestureService.BindKeyGestures((UIElement) view);
            base.OnViewLoaded(view);
        }
#pragma warning disable 649
        [Import] private IShell _shell;

        [Import] private IResourceManager _resourceManager;

        [Import] private ICommandKeyGestureService _commandKeyGestureService;
#pragma warning restore 649
    }
}