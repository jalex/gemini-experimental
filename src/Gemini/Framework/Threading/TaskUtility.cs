#region

using System.Threading.Tasks;

#endregion

namespace Gemini.Framework.Threading
{
    public class TaskUtility
    {
        public static readonly Task Completed = Task.FromResult(true);
    }
}
