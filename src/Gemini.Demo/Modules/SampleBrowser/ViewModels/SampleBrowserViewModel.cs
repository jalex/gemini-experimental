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

        [ImportingConstructor]
        public SampleBrowserViewModel([Import] IShell shell,
            [ImportMany] ISample[] samples)
        {
            _shell = shell;
            Samples = samples;
        }

        public override string DisplayName => "Sample Browser";

        public ISample[] Samples { get; }

        public void Activate(ISample sample)
        {
            sample.Activate(_shell);
        }
    }
}