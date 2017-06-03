#region

using System;

#endregion

namespace Gemini.Framework.Commands
{
    /// <summary>
    ///     Represents a service for managing executable operations.
    /// </summary>
    public interface ICommandService
    {
        /// <summary>
        ///     Resolves a <see cref="CommandDefinitionBase" /> for the specified type contract.
        /// </summary>
        /// <param name="commandDefinitionType">The type contract of a <see cref="CommandDefinitionBase" />.</param>
        /// <returns>The <see cref="CommandDefinitionBase" /> if available, otherwise null.</returns>
        CommandDefinitionBase GetCommandDefinition(Type commandDefinitionType);

        /// <summary>
        ///     Returns a <see cref="Command" /> for the specified <see cref="CommandDefinitionBase" />.
        /// </summary>
        /// <param name="commandDefinition">The <see cref="CommandDefinitionBase" /> to lookup.</param>
        /// <returns>A <see cref="Command" />, or null.</returns>
        Command GetCommand(CommandDefinitionBase commandDefinition);

        /// <summary>
        ///     Returns a <see cref="TargetableCommand" /> created using the specified <see cref="Command" />.
        /// </summary>
        /// <param name="command">The <see cref="Command" /> to create the targetable command from.</param>
        /// <returns>The <see cref="TargetableCommand" />.</returns>
        TargetableCommand GetTargetableCommand(Command command);
    }
}
