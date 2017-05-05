#region

using System;
using System.ComponentModel.Composition;

#endregion

namespace Gemini.Framework.Commands
{

    /// <summary>
    ///     Represents an <see cref="ExportAttribute"/> for annotating a <see cref="CommandDefinitionBase"/> export.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class CommandDefinitionAttribute : ExportAttribute
    {
        /// <summary>
        ///     Creates a new <see cref="CommandDefinitionAttribute"/>.
        /// </summary>
        public CommandDefinitionAttribute()
            : base(typeof(CommandDefinitionBase))
        {
        }
    }
}
