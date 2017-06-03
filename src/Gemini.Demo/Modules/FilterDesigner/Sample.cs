#region

using System.ComponentModel.Composition;
using Caliburn.Micro;
using Gemini.Demo.Modules.FilterDesigner.ViewModels;
using Gemini.Demo.Modules.SampleBrowser;
using Gemini.Framework.Services;
using Gemini.Modules.Inspector;
using Gemini.Modules.Toolbox;

#endregion

namespace Gemini.Demo.Modules.FilterDesigner
{
    /// <summary>
    ///     Represents a default implementation of the <see cref="ISample" />.
    /// </summary>
    [Export(typeof(ISample))]
    public class Sample : ISample
    {
        /// <summary>
        ///     Returns the name of the example.
        /// </summary>
        public string Name => "Filter Designer";

        /// <summary>
        ///     Activates the example using the specified <see cref="IShell" />.
        /// </summary>
        /// <param name="shell">The <see cref="IShell" />.</param>
        public void Activate(IShell shell)
        {
            shell.OpenDocument(IoC.Get<GraphViewModel>());
            shell.ShowTool<IInspectorTool>();
            shell.ShowTool<IToolbox>();
        }
    }
}
