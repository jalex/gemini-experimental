#region

using System;
using System.ComponentModel.Composition;
using Caliburn.Micro;

#endregion

namespace Gemini.Framework.Results
{
    public class ShowWindowResult<TWindow> : OpenResultBase<TWindow>
        where TWindow : IWindow
    {
        private readonly Func<TWindow> _windowLocator = () => IoC.Get<TWindow>();

        public ShowWindowResult()
        {
        }

        public ShowWindowResult(TWindow window)
        {
            _windowLocator = () => window;
        }

        [Import]
        public IWindowManager WindowManager { get; set; }

        public override void Execute(CoroutineExecutionContext context)
        {
            var window = _windowLocator();

            if (SetData != null)
                SetData(window);

            if (_onConfigure != null)
                _onConfigure(window);

            window.Deactivated += (s, e) =>
            {
                if (!e.WasClosed)
                    return;

                if (_onShutDown != null)
                    _onShutDown(window);

                OnCompleted(null, false);
            };

            WindowManager.ShowWindow(window);
        }
    }
}