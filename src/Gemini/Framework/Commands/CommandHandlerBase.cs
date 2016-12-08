#region

using System.Threading.Tasks;

#endregion

namespace Gemini.Framework.Commands
{
    public abstract class CommandHandlerBase<TCommandDefinition> : ICommandHandler<TCommandDefinition>
        where TCommandDefinition : CommandDefinition
    {
        public virtual void Update(Command command)
        {
        }

        public abstract Task Run(Command command);
    }
}