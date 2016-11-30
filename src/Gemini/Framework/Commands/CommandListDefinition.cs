using System;

namespace Gemini.Framework.Commands
{
    public abstract class CommandListDefinition : CommandDefinitionBase
    {
        public override sealed string Text => "[NotUsed]";

        public override sealed string ToolTip => "[NotUsed]";

        public override sealed Uri IconSource => null;

        public override sealed bool IsList => true;
    }
}