#region

using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Caliburn.Micro;
using Gemini.Demo.Modules.FilterDesigner.ViewModels;
using Gemini.Framework.Commands;
using Gemini.Framework.Services;
using Gemini.Modules.Inspector;

#endregion

namespace Gemini.Demo.Modules.FilterDesigner.Commands
{

    /// <summary>
    ///     Represents the <see cref="ICommandHandler{TCommandDefinition}"/> for the <see cref="OpenGraphCommandDefinition"/>.
    /// </summary>
    [CommandHandler]
    public class OpenGraphCommandHandler : CommandHandlerBase<OpenGraphCommandDefinition>
    {
        private readonly IShell _shell;

        /// <summary>
        ///     Creates a new <see cref="OpenGraphCommandHandler"/>.
        /// </summary>
        /// <param name="shell">The <see cref="IShell"/> for opening the graph document.</param>
        [ImportingConstructor]
        public OpenGraphCommandHandler(IShell shell)
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
            _shell.OpenDocument(new GraphViewModel(IoC.Get<IInspectorTool>()));
            return Task.CompletedTask;
        }
    }
}
