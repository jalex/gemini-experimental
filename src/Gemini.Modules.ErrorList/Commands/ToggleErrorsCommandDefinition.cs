using System;
using Gemini.Framework.Commands;

namespace Gemini.Modules.ErrorList.Commands
{
    [CommandDefinition]
    public class ToggleErrorsCommandDefinition : CommandDefinition
    {
        public const string CommandName = "ErrorList.ToggleErrors";

        public override string Name => CommandName;

        public override string Text => "[NotUsed]";

        public override string ToolTip => "[NotUsed]";

        public override Uri IconSource => new Uri("pack://application:,,,/Gemini.Modules.ErrorList;component/Resources/Error.png");
    }
}