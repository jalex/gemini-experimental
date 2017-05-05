#region

using System;
using System.Diagnostics;
using System.Windows.Input;

#endregion

namespace Gemini.Framework
{
    /// <summary>
    ///     Used where Caliburn.Micro needs to be interfaced to ICommand.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Predicate<object> _canExecute;

        private readonly Action<object> _execute;

        /// <summary>
        ///     Creates a new <see cref="RelayCommand" />.
        /// </summary>
        /// <param name="execute">The action delegate to execute.</param>
        /// <param name="canExecute">A <see cref="Predicate{T}" /> to determine whether the command should execute.</param>
        /// <exception cref="ArgumentNullException"><paramref name="execute" /> is <see langword="null" /></exception>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute));

            _execute = execute;
            _canExecute = canExecute;
        }


        /// <summary>Defines the method that determines whether the command can execute in its current state.</summary>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to null.
        /// </param>
        [DebuggerStepThrough]
        public bool CanExecute(object parameter) => (_canExecute == null) || _canExecute(parameter);

        /// <summary>Occurs when changes occur that affect whether or not the command should execute.</summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>Defines the method to be called when the command is invoked.</summary>
        /// <param name="parameter">
        ///     Data used by the command.  If the command does not require data to be passed, this object can
        ///     be set to null.
        /// </param>
        public void Execute(object parameter) => _execute(parameter);
    }
}
