#region

using System;
using Gemini.Framework.Commands;

#endregion

namespace Gemini.Modules.ErrorList.Commands
{
    [CommandDefinition]
    public class ToggleMessagesCommandDefinition : CommandDefinition
    {
        public const string CommandName = "ErrorList.ToggleMessages";

        public override string Name => CommandName;

        public override string Text => "[NotUsed]";

        public override string ToolTip => "[NotUsed]";

        public override Uri IconSource
            => new Uri("pack://application:,,,/Gemini.Modules.ErrorList;component/Resources/Message.png");
    }
}
