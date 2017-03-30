#region

using System;

#endregion

namespace Gemini.Framework.Commands
{

    /// <summary>
    ///     Represents a definition of a command list. Command list definitions can be used for
    ///     declaring commands which are being dynamically created using an associated
    ///     <see cref="ICommandListHandler{TCommandListDefinition}"/>.
    /// </summary>
    public abstract class CommandListDefinition : CommandDefinitionBase
    {
        /// <summary>
        ///     Returns the display text of the command.
        /// </summary>
        public sealed override string Text => "[NotUsed]";

        /// <summary>
        ///     Returns a tooltip associated with the command.
        /// </summary>
        public sealed override string ToolTip => "[NotUsed]";

        /// <summary>
        ///     Returns the <see cref="Uri"/> of an icon associated with the command.
        /// </summary>
        public sealed override Uri IconSource => null;

        /// <summary>
        ///     Returns whether the current definition represents a list command.
        /// </summary>
        public sealed override bool IsList => true;
    }
}
