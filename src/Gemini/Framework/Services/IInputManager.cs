#region

using System.Windows;
using System.Windows.Input;

#endregion

namespace Gemini.Framework.Services
{
    public interface IInputManager
    {
        void SetShortcut(DependencyObject view, InputGesture gesture, object handler);
        void SetShortcut(InputGesture gesture, object handler);
    }
}
