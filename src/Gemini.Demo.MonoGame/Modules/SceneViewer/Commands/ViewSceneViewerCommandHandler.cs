#region

using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Gemini.Demo.MonoGame.Modules.SceneViewer.ViewModels;
using Gemini.Framework.Commands;
using Gemini.Framework.Services;
using Gemini.Framework.Threading;

#endregion

namespace Gemini.Demo.MonoGame.Modules.SceneViewer.Commands
{

    /// <summary>
    ///     Represents an <see cref="ICommandHandler{TCommandDefinition}"/> implementation for the
    ///     <see cref="ViewSceneViewerCommandDefinition"/>.
    /// </summary>
    [CommandHandler]
    public class ViewSceneViewerCommandHandler : CommandHandlerBase<ViewSceneViewerCommandDefinition>
    {
        private readonly IShell _shell;

        /// <summary>
        ///     Creates a new <see cref="ViewSceneViewerCommandHandler"/>.
        /// </summary>
        /// <param name="shell">The <see cref="IShell"/> for opening the scene viewer.</param>
        [ImportingConstructor]
        public ViewSceneViewerCommandHandler(IShell shell)
        {
            _shell = shell;
        }

        /// <summary>
        ///     Asynchronously handles the execution of the specified <see cref="Command"/>.
        /// </summary>
        /// <param name="command">The <see cref="Command"/> to execute.</param>
        /// <returns>A <see cref="Task"/> representing the operation.</returns>
        public override Task Run(Command command)
        {
            _shell.OpenDocument(new SceneViewModel());
            return Task.CompletedTask;
        }

    }
}
