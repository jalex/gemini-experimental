#region

using Gemini.Framework.Services;

#endregion

namespace Gemini.Demo.Modules.SampleBrowser
{

    /// <summary>
    ///     Represents an example.
    /// </summary>
    public interface ISample
    {
        /// <summary>
        ///     Returns the name of the example.
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     Activates the example using the specified <see cref="IShell"/>.
        /// </summary>
        /// <param name="shell">The <see cref="IShell"/>.</param>
        void Activate(IShell shell);
    }
}
