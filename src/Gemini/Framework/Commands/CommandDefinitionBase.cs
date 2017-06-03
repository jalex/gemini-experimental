#region

using System;

#endregion

namespace Gemini.Framework.Commands
{
    /// <summary>
    ///     Represents a base type for defining executable operations within the Gemini framework.
    /// </summary>
    public abstract class CommandDefinitionBase
    {
        /// <summary>
        ///     Returns the name of the command.
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        ///     Returns the display text of the command.
        /// </summary>
        public virtual string Text => Name;

        /// <summary>
        ///     Returns a tooltip associated with the command.
        /// </summary>
        public virtual string ToolTip => Name;

        /// <summary>
        ///     Returns the <see cref="Uri" /> of an icon associated with the command.
        /// </summary>
        public virtual Uri IconSource { get; } = null;

        /// <summary>
        ///     Returns whether the current definition represents a list command.
        /// </summary>
        public abstract bool IsList { get; }
    }
}
