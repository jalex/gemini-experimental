#region

using System;
using System.ComponentModel.Composition;

#endregion

namespace Gemini.Framework.Commands
{
    /// <summary>
    ///     Represents an <see cref="ExportAttribute"/> for annotating a <see cref="ICommandHandler"/> export.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class CommandHandlerAttribute : ExportAttribute
    {
        /// <summary>
        ///     Creates a new <see cref="CommandHandlerAttribute"/>.
        /// </summary>
        public CommandHandlerAttribute()
            : base(typeof(ICommandHandler))
        {
        }
    }
}
