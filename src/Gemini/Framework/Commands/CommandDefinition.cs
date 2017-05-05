#region

using System;

#endregion

namespace Gemini.Framework.Commands
{

    /// <summary>
    ///     Represents a base type for standard command definitions.
    /// </summary>
    public abstract class CommandDefinition : CommandDefinitionBase
    {
        /// <summary>
        ///     Returns the <see cref="Uri"/> of an icon associated with the command.
        /// </summary>
        public override Uri IconSource => null;

        /// <summary>
        ///     Returns whether the current definition represents a list command.
        /// </summary>
        public sealed override bool IsList => false;
    }
}
