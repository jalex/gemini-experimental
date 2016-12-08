#region

using System.Threading.Tasks;

#endregion

namespace Gemini.Framework.Commands
{
    public interface ICommandHandler<TCommandDefinition> : ICommandHandler
        where TCommandDefinition : CommandDefinition
    {
        void Update(Command command);
        Task Run(Command command);
    }

    public interface ICommandHandler
    {
    }

    public interface ICommandListHandler : ICommandHandler
    {
    }
}