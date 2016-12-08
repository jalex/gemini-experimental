#region

using System.ComponentModel.Composition;
using Gemini.Framework;
using Gemini.Framework.Services;

#endregion

namespace Gemini.Demo.Modules.SampleBrowser.ViewModels
{
    [Export(typeof(SampleBrowserViewModel))]
    public class SampleBrowserViewModel : Document
    {
        private readonly IShell _shell;

        public override string DisplayName => "Sample Browser";

        public ISample[] Samples { get; }

        [ImportingConstructor]
        public SampleBrowserViewModel([Import] IShell shell,
            [ImportMany] ISample[] samples)
        {
            _shell = shell;
            Samples = samples;
        }

        public void Activate(ISample sample)
        {
            sample.Activate(_shell);
        }
    }
}