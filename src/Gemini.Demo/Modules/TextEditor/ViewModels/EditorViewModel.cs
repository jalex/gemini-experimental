using System;
using System.IO;
using System.Threading.Tasks;
using Gemini.Demo.Modules.TextEditor.Views;
using Gemini.Framework;
using Gemini.Framework.Threading;
using System.ComponentModel.Composition;

namespace Gemini.Demo.Modules.TextEditor.ViewModels
{
    [Export(typeof(EditorViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
#pragma warning disable 659
    public class EditorViewModel : PersistedDocument
#pragma warning restore 659
    {
        private EditorView _view;
		private string _originalText;
        private bool _notYetLoaded = false;

        protected override Task DoNew()
        {
            _originalText = string.Empty;
            ApplyOriginalText();
            return TaskUtility.Completed;
        }

        protected override Task DoLoad(string filePath)
        {
            _originalText = File.ReadAllText(filePath);
            ApplyOriginalText();
            return TaskUtility.Completed;
        }

        protected override Task DoSave(string filePath)
        {
            var newText = _view.TextBox.Text;
            File.WriteAllText(filePath, newText);
            _originalText = newText;
            return TaskUtility.Completed;
        }

        private void ApplyOriginalText()
        {
            // At StartUp, _view is null, so notYetLoaded flag is added
            if (_view == null)
            {
                _notYetLoaded = true;
                return;
            }
            _view.TextBox.Text = _originalText;

            _view.TextBox.TextChanged += delegate
            {
                IsDirty = string.Compare(_originalText, _view.TextBox.Text) != 0;
            };
        }

		protected override void OnViewLoaded(object view)
		{
            _view = (EditorView) view;

            if (_notYetLoaded)
            {
                ApplyOriginalText();
                _notYetLoaded = false;
            }
		}

        public override bool Equals(object obj)
		{
			var other = obj as EditorViewModel;
            return other != null
                && string.Equals(FilePath, other.FilePath, StringComparison.InvariantCultureIgnoreCase)
                && string.Equals(FileName, other.FileName, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}