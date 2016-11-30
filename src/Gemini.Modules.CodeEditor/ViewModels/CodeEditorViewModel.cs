#region

using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Threading.Tasks;
using Caliburn.Micro;
using Gemini.Framework;
using Gemini.Framework.Threading;
using Gemini.Modules.CodeEditor.Views;
using Gemini.Modules.StatusBar;

#endregion

namespace Gemini.Modules.CodeEditor.ViewModels
{
    [Export(typeof(CodeEditorViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
#pragma warning disable 659
    public class CodeEditorViewModel : PersistedDocument
#pragma warning restore 659
    {
        private readonly LanguageDefinitionManager _languageDefinitionManager;
        private bool _notYetLoaded;
        private string _originalText;
        private IStatusBar _statusBar;
        private ICodeEditorView _view;

        [ImportingConstructor]
        public CodeEditorViewModel(LanguageDefinitionManager languageDefinitionManager)
        {
            _languageDefinitionManager = languageDefinitionManager;
        }

        protected override void OnViewLoaded(object view)
        {
            _view = (ICodeEditorView) view;
            _statusBar = IoC.Get<IStatusBar>();

            if (_notYetLoaded)
            {
                ApplyOriginalText();
                _notYetLoaded = false;
            }
        }

        public override bool Equals(object obj)
        {
            var other = obj as CodeEditorViewModel;
            return (other != null)
                   && string.Equals(FilePath, other.FilePath, StringComparison.InvariantCultureIgnoreCase)
                   && string.Equals(FileName, other.FileName, StringComparison.InvariantCultureIgnoreCase);
        }

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
            var newText = _view.TextEditor.Text;
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
            _view.TextEditor.Text = _originalText;

            _view.TextEditor.TextChanged +=
                delegate { IsDirty = string.Compare(_originalText, _view.TextEditor.Text) != 0; };

            UpdateStatusBar();

            // To update status bar items, Caret PositionChanged event is added
            _view.TextEditor.TextArea.Caret.PositionChanged += delegate { UpdateStatusBar(); };

            var fileExtension = Path.GetExtension(FileName).ToLower();

            var languageDefinition = _languageDefinitionManager.GetDefinitionByExtension(fileExtension);

            SetLanguage(languageDefinition);
        }

        /// <summary>
        ///     Update Column and Line position properties when caret position is changed
        /// </summary>
        private void UpdateStatusBar()
        {
            var lineNumber = _view.TextEditor.Document.GetLineByOffset(_view.TextEditor.CaretOffset).LineNumber;
            var colPosition = _view.TextEditor.TextArea.Caret.VisualColumn + 1;

            // TODO: Now I don't know about Ch#
            //int charPosition = _view.TextEditor.CaretOffset;

            if ((_statusBar != null) && (_statusBar.Items.Count >= 3))
            {
                _statusBar.Items[1].Message = $"Ln {lineNumber}";
                _statusBar.Items[2].Message = $"Col {colPosition}";
            }
        }

        private void SetLanguage(ILanguageDefinition languageDefinition)
        {
            _view.TextEditor.SyntaxHighlighting = languageDefinition != null
                ? languageDefinition.SyntaxHighlighting
                : null;
        }
    }
}