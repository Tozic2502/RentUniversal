using System;
using System.Windows.Input;

namespace RentalSystem.AdminClient.ViewModel
{
    
    /// <summary>
    /// A reusable ICommand implementation for MVVM command bindings.
    /// </summary>
    /// <remarks>
    /// Allows ViewModels to expose commands without code-behind.
    /// Supports optional execution conditions via <c>canExecute</c>.
    /// </remarks>
    public class RelayCommand : ICommand
    {
        private readonly Action<object?> _execute;
        private readonly Predicate<object?>? _canExecute;

        /// <summary>
        /// Creates a new RelayCommand.
        /// </summary>
        /// <param name="execute">Action to execute when the command is invoked.</param>
        /// <param name="canExecute">
        /// Optional predicate that determines whether the command can execute.
        /// </param>
        public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute(parameter);
        }

        public event EventHandler? CanExecuteChanged
        {
            add    { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}