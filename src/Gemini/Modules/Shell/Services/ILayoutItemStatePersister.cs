#region

using Gemini.Framework.Services;
using Gemini.Modules.Shell.Views;

#endregion

namespace Gemini.Modules.Shell.Services
{
    public interface ILayoutItemStatePersister
    {
        bool SaveState(IShell shell, IShellView shellView, string fileName);
        bool LoadState(IShell shell, IShellView shellView, string fileName);
    }
}