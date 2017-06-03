namespace Gemini.Framework.Commands
{
    /// <summary>
    ///     Represents an updatable UI item associated with a <see cref="CommandDefinitionBase" />.
    /// </summary>
    public interface ICommandUiItem
    {
        /// <summary>
        ///     Returns the <see cref=" CommandDefinitionBase" /> of the UI item.
        /// </summary>
        CommandDefinitionBase CommandDefinition { get; }

        /// <summary>
        ///     Updates the UI item using the specified <see cref="CommandHandlerWrapper" />.
        /// </summary>
        /// <param name="commandHandler">The <see cref="CommandHandlerWrapper" /> for updating the UI item.</param>
        void Update(CommandHandlerWrapper commandHandler);
    }
}
