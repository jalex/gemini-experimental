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
    [Export(typeof(ISample))]
    public class Sample : ISample
    {
        public string Name => "Filter Designer";

        public void Activate(IShell shell)
        {
            shell.OpenDocument(IoC.Get<GraphViewModel>());
            shell.ShowTool<IInspectorTool>();
            shell.ShowTool<IToolbox>();
        }
    }
}