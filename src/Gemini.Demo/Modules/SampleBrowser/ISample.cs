#region

using Gemini.Framework.Services;

#endregion

namespace Gemini.Demo.Modules.SampleBrowser
{
    public interface ISample
    {
        string Name { get; }
        void Activate(IShell shell);
    }
}