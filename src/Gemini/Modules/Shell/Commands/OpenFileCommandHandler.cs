﻿#region

using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Caliburn.Micro;
using Gemini.Framework;
using Gemini.Framework.Commands;
using Gemini.Framework.Services;
using Gemini.Properties;
using Microsoft.Win32;

#endregion

namespace Gemini.Modules.Shell.Commands
{
    [CommandHandler]
    public class OpenFileCommandHandler : CommandHandlerBase<OpenFileCommandDefinition>
    {
        private readonly IEditorProvider[] _editorProviders;
        private readonly IShell _shell;

        [ImportingConstructor]
        public OpenFileCommandHandler(IShell shell, [ImportMany] IEditorProvider[] editorProviders)
        {
            _shell = shell;
            _editorProviders = editorProviders;
        }

        public override async Task Run(Command command)
        {
            var dialog = new OpenFileDialog
            {
                Filter = Resources.AllSupportedFiles + "|" + string.Join(";", _editorProviders
                             .SelectMany(x => x.FileTypes).Select(x => "*" + x.FileExtension))
            };


            dialog.Filter += "|" + string.Join("|", _editorProviders
                                 .SelectMany(x => x.FileTypes)
                                 .Select(x => x.Name + "|*" + x.FileExtension));

            if (dialog.ShowDialog() == true)
            {
                var fullPath = Path.GetFullPath(dialog.FileName);

                if (!_shell.TryActivateDocumentByPath(fullPath))
                    _shell.OpenDocument(await GetEditor(fullPath));

                // Add the file to the recent documents list
                _shell.RecentFiles.Update(fullPath);
            }
        }

        public static Task<IDocument> GetEditor(string path)
        {
            var provider = IoC.GetAllInstances(typeof(IEditorProvider))
                .Cast<IEditorProvider>()
                .FirstOrDefault(p => p.Handles(path));
            if (provider == null)
                return null;

            var editor = provider.Create();

            var viewAware = (IViewAware) editor;
            viewAware.ViewAttached += (sender, e) =>
            {
                var frameworkElement = (FrameworkElement) e.View;

                RoutedEventHandler loadedHandler = null;
                loadedHandler = async (sender2, e2) =>
                {
                    frameworkElement.Loaded -= loadedHandler;
                    await provider.Open(editor, path);
                };
                frameworkElement.Loaded += loadedHandler;
            };

            return Task.FromResult(editor);
        }
    }
}
