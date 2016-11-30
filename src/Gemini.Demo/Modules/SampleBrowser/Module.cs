#region

using System.ComponentModel.Composition;
using Caliburn.Micro;
using Gemini.Demo.Modules.SampleBrowser.ViewModels;
using Gemini.Framework;

#endregion

namespace Gemini.Demo.Modules.SampleBrowser
{
    [Export(typeof(IModule))]
    public class Module : ModuleBase
    {
        public override void PostInitialize()
        {
            Shell.OpenDocument(IoC.Get<SampleBrowserViewModel>());
        }
    }
}