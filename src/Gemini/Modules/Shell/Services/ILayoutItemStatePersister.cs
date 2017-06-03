#region

using Gemini.Framework.Services;
using Gemini.Modules.Shell.Views;

#endregion

namespace Gemini.Modules.Shell.Services
{
    /// <summary>
    ///     Represents a service for persisting the state of the workspace of the <see cref="IShell" />.
    /// </summary>
    public interface ILayoutItemStatePersister
    {
        /// <summary>
        ///     Persists the state of the workspace into a file.
        /// </summary>
        /// <param name="shell">The <see cref="IShell" />.</param>
        /// <param name="shellView">The <see cref="IShellView" />.</param>
        /// <param name="fileName">The path of the file into where the state of the workspace will be persisted.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        bool SaveState(IShell shell, IShellView shellView, string fileName);

        /// <summary>
        ///     Loads the state of the workspace from a file.
        /// </summary>
        /// <param name="shell">The <see cref="IShell" />.</param>
        /// <param name="shellView">The <see cref="IShellView" />.</param>
        /// <param name="fileName">The path of the file from where the state of the workspace will be loaded.</param>
        /// <returns>True if the operation was successful, otherwise false.</returns>
        bool LoadState(IShell shell, IShellView shellView, string fileName);
    }
}
