#region

using Caliburn.Micro;

#endregion

namespace Gemini.Framework
{
    /// <summary>
    ///     Represents an application window.
    /// </summary>
    public interface IWindow : IActivate, IDeactivate, INotifyPropertyChangedEx
    {
    }
}
