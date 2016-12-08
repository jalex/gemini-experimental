#region

using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

namespace Gemini.Framework.Commands
{
    public interface ICommandListHandler<TCommandDefinition> : ICommandHandler
        where TCommandDefinition : CommandListDefinition
    {
        void Populate(Command command, List<Command> commands);
        Task Run(Command command);
    }
}