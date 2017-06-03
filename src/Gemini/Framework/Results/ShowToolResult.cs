#region

using System;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using Gemini.Framework.Services;

#endregion

namespace Gemini.Framework.Results
{
    public class ShowToolResult<TTool> : OpenResultBase<TTool>
        where TTool : ITool
    {
        private readonly Func<TTool> _toolLocator = () => IoC.Get<TTool>();

#pragma warning disable 649
        [Import] private IShell _shell;
#pragma warning restore 649

        public ShowToolResult()
        {
        }

        public ShowToolResult(TTool tool)
        {
            _toolLocator = () => tool;
        }

        public override void Execute(CoroutineExecutionContext context)
        {
            var tool = _toolLocator();

            SetData?.Invoke(tool);

            _onConfigure?.Invoke(tool);

            tool.Deactivated += (s, e) =>
            {
                if (!e.WasClosed)
                    return;

                _onShutDown?.Invoke(tool);

                OnCompleted(null, false);
            };

            _shell.ShowTool(tool);
        }
    }
}
