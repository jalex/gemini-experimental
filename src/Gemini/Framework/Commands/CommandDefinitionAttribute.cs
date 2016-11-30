#region

using System;
using System.ComponentModel.Composition;

#endregion

namespace Gemini.Framework.Commands
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class CommandDefinitionAttribute : ExportAttribute
    {
        public CommandDefinitionAttribute()
            : base(typeof(CommandDefinitionBase))
        {
        }
    }
}