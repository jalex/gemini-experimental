#region

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace Gemini.Framework.Commands
{
    /// <summary>
    ///     Represents a marker interface for command list handlers.
    /// </summary>
    /// <remarks>
    ///     Don't implement this contract directly. Instead, refer to the generic
    ///     <see cref="ICommandListHandler{TCommandDefinition}" /> contract.
    /// </remarks>
    public interface ICommandListHandler : ICommandHandler
    {
    }

    /// <summary>
    ///     Represents a service for handling command lists, which are usually dynamic commands generated
    ///     during application runtime.
    /// </summary>
    /// <typeparam name="TCommandListDefinition">
    ///     The type of the command list definition this handler is associated with,
    ///     implementing <see cref="CommandListDefinition" />.
    /// </typeparam>
    public interface ICommandListHandler<TCommandListDefinition> : ICommandHandler
        where TCommandListDefinition : CommandListDefinition
    {
        /// <summary>
        ///     Populates a list of dynamic <see cref="Command" />s for this list command.
        /// </summary>
        /// <param name="command">The <see cref="Command" /> state for the current definition.</param>
        /// <param name="commands">A <see cref="List{T}" /> of <see cref="Command" />s to populate.</param>
        void Populate(Command command, List<Command> commands);

        /// <summary>
        ///     Asynchronously handles the execution of the specified <see cref="Command" />.
        /// </summary>
        /// <param name="command">The <see cref="Command" /> to execute.</param>
        /// <returns>A <see cref="Task" /> representing the operation.</returns>
        Task Run(Command command);
    }
}
