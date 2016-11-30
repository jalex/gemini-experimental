#region

using System;
using Gemini.Framework.Commands;

#endregion

namespace Gemini.Modules.ErrorList.Commands
{
    [CommandDefinition]
    public class ToggleWarningsCommandDefinition : CommandDefinition
    {
        public const string CommandName = "ErrorList.ToggleWarnings";

        public override string Name => CommandName;

        public override string Text => "[NotUsed]";

        public override string ToolTip => "[NotUsed]";

        public override Uri IconSource
            => new Uri("pack://application:,,,/Gemini.Modules.ErrorList;component/Resources/Warning.png");
    }
}