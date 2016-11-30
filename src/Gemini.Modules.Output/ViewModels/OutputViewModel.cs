#region

using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Text;
using Caliburn.Micro;
using Gemini.Framework;
using Gemini.Framework.Services;
using Gemini.Modules.Output.Properties;
using Gemini.Modules.Output.Views;

#endregion

namespace Gemini.Modules.Output.ViewModels
{
    [Export(typeof(IOutput))]
    public class OutputViewModel : Tool, IOutput
    {
        private readonly StringBuilder _stringBuilder;
        private readonly OutputWriter _writer;
        private IOutputView _view;

        public OutputViewModel()
        {
            DisplayName = Resources.OutputDisplayName;
            _stringBuilder = new StringBuilder();
            _writer = new OutputWriter(this);
        }

        public override PaneLocation PreferredLocation => PaneLocation.Bottom;

        public TextWriter Writer => _writer;

        public void Clear()
        {
            if (_view != null)
                Execute.OnUIThread(() => _view.Clear());
            _stringBuilder.Clear();
        }

        public void AppendLine(string text)
        {
            Append(text + Environment.NewLine);
        }

        public void Append(string text)
        {
            _stringBuilder.Append(text);
            OnTextChanged();
        }

        private void OnTextChanged()
        {
            if (_view != null)
                Execute.OnUIThread(() => _view.SetText(_stringBuilder.ToString()));
        }

        protected override void OnViewLoaded(object view)
        {
            _view = (IOutputView) view;
            _view.SetText(_stringBuilder.ToString());
            _view.ScrollToEnd();
        }
    }
}