#region

using System.Threading.Tasks;
using System.Windows;
using Gemini.Framework.Commands;
using Gemini.Framework.Threading;

#endregion

namespace Gemini.Modules.Shell.Commands
{
    [CommandHandler]
    public class ViewFullScreenCommandHandler : CommandHandlerBase<ViewFullScreenCommandDefinition>
    {
        public override Task Run(Command command)
        {
            var window = Application.Current.MainWindow;
            if (window == null)
                return TaskUtility.Completed;

            if (window.WindowState != WindowState.Maximized)
            {
                window.WindowState = WindowState.Maximized;
                window.WindowStyle = WindowStyle.None;
            }

            else
            {
                window.WindowState = WindowState.Normal;
                window.WindowStyle = WindowStyle.SingleBorderWindow;
            }

            return TaskUtility.Completed;
        }
    }
}