#region

using System.Threading.Tasks;

#endregion

namespace Gemini.Framework.Commands
{

    /// <summary>
    ///     Represents a base implementation of a <see cref="ICommandHandler{TCommandDefinition}"/>.
    /// </summary>
    /// <typeparam name="TCommandDefinition">
    ///     The type of the command definition this handler is associated with,
    ///     implementing <see cref="CommandDefinition"/>.
    /// </typeparam>
    public abstract class CommandHandlerBase<TCommandDefinition> : ICommandHandler<TCommandDefinition>
        where TCommandDefinition : CommandDefinition
    {
        /// <summary>
        ///     Updates the state of the specified <see cref="Command"/>.
        /// </summary>
        /// <param name="command">The <see cref="Command"/> to update.</param>
        public virtual void Update(Command command)
        {
        }

        /// <summary>
        ///     Asynchronously handles the execution of the specified <see cref="Command"/>.
        /// </summary>
        /// <param name="command">The <see cref="Command"/> to execute.</param>
        /// <returns>A <see cref="Task"/> representing the operation.</returns>
        public abstract Task Run(Command command);
    }
}
