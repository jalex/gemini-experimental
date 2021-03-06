﻿#region

using System;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using Gemini.Framework.Services;
using Gemini.Modules.Shell.Commands;

#endregion

namespace Gemini.Framework.Results
{
    public class OpenDocumentResult : OpenResultBase<IDocument>
    {
        private readonly IDocument _editor;
        private readonly Type _editorType;
        private readonly string _path;

#pragma warning disable 649
        [Import] private IShell _shell;
#pragma warning restore 649

        public OpenDocumentResult(IDocument editor)
        {
            _editor = editor;
        }

        public OpenDocumentResult(string path)
        {
            _path = path;
        }

        public OpenDocumentResult(Type editorType)
        {
            _editorType = editorType;
        }

        public override void Execute(CoroutineExecutionContext context)
        {
            var editor = _editor ??
                         (string.IsNullOrEmpty(_path)
                             ? (IDocument) IoC.GetInstance(_editorType, null)
                             : GetEditor(_path));

            if (editor == null)
            {
                OnCompleted(null, true);
                return;
            }

            SetData?.Invoke(editor);

            _onConfigure?.Invoke(editor);

            editor.Deactivated += (s, e) =>
            {
                if (!e.WasClosed)
                    return;

                _onShutDown?.Invoke(editor);
            };

            _shell.OpenDocument(editor);

            OnCompleted(null, false);
        }

        private static IDocument GetEditor(string path)
        {
            return OpenFileCommandHandler.GetEditor(path).Result;
        }
    }
}
