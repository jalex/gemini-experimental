#region

using System;
using System.ComponentModel.Composition;

#endregion

namespace Gemini.Framework.Commands
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class CommandHandlerAttribute : ExportAttribute
    {
        public CommandHandlerAttribute()
            : base(typeof(ICommandHandler))
        {
        }
    }
}