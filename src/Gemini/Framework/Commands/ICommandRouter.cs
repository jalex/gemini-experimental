namespace Gemini.Framework.Commands
{
    /// <summary>
    ///     Represents a service for creating <see cref="CommandHandlerWrapper" />s from <see cref="CommandDefinitionBase" />s.
    /// </summary>
    public interface ICommandRouter
    {
        /// <summary>
        ///     Returns the <see cref="CommandHandlerWrapper" /> for invoking an underlying command handler
        ///     associated with the specified <see cref="CommandDefinitionBase" />.
        /// </summary>
        /// <param name="commandDefinition">The <see cref="CommandDefinitionBase" /> for lookup up the command handler.</param>
        /// <returns>A <see cref="CommandHandlerWrapper" /> instance, or null if not available.</returns>
        CommandHandlerWrapper GetCommandHandler(CommandDefinitionBase commandDefinition);
    }
}
