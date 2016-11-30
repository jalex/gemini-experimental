#region

using System.Windows;
using System.Windows.Input;

#endregion

namespace Gemini.Framework.Commands
{
    public interface ICommandKeyGestureService
    {
        void BindKeyGestures(UIElement uiElement);
        KeyGesture GetPrimaryKeyGesture(CommandDefinitionBase commandDefinition);
    }
}