#region

using System;
using System.ComponentModel.Composition;
using Caliburn.Micro;

#endregion

namespace Gemini.Framework.Results
{
    public class ShowDialogResult<TWindow> : OpenResultBase<TWindow>
        where TWindow : IWindow
    {
        private readonly Func<TWindow> _windowLocator = () => IoC.Get<TWindow>();

        [Import]
        public IWindowManager WindowManager { get; set; }

        public ShowDialogResult()
        {
        }

        public ShowDialogResult(TWindow window)
        {
            _windowLocator = () => window;
        }

        public override void Execute(CoroutineExecutionContext context)
        {
            var window = _windowLocator();

            SetData?.Invoke(window);

            _onConfigure?.Invoke(window);

            var result = WindowManager.ShowDialog(window).GetValueOrDefault();

            _onShutDown?.Invoke(window);

            OnCompleted(null, !result);
        }
    }
}