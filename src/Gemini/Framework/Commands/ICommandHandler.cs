#region

using System.Threading.Tasks;

#endregion

namespace Gemini.Framework.Commands
{
    /// <summary>
    ///     Represents a service which handles the execution of a <see cref="Command" />.
    /// </summary>
    /// <typeparam name="TCommandDefinition">
    ///     The type of the command definition this handler is associated with,
    ///     implementing <see cref="CommandDefinition" />.
    /// </typeparam>
    public interface ICommandHandler<TCommandDefinition> : ICommandHandler
        where TCommandDefinition : CommandDefinition
    {
        /// <summary>
        ///     Updates the state of the specified <see cref="Command" />.
        /// </summary>
        /// <param name="command">The <see cref="Command" /> to update.</param>
        void Update(Command command);

        /// <summary>
        ///     Asynchronously handles the execution of the specified <see cref="Command" />.
        /// </summary>
        /// <param name="command">The <see cref="Command" /> to execute.</param>
        /// <returns>A <see cref="Task" /> representing the operation.</returns>
        Task Run(Command command);
    }

    /// <summary>
    ///     Represents a marker interface for command handlers.
    /// </summary>
    /// <remarks>
    ///     Don't implement this contract directly. Instead, refer to the generic
    ///     <see cref="ICommandHandler{TCommandDefinition}" /> and <see cref="ICommandListHandler{TCommandDefinition}" />
    ///     contracts.
    /// </remarks>
    public interface ICommandHandler
    {
    }
}
