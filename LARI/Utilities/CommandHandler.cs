using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LARI.Utilities
{
    /// <summary>
    /// Class to handle commands (eg. buttons)
    /// </summary>
    public class CommandHandler : ICommand
    {
        private Action action;
        private bool canExecute;

        /// <summary>
        /// Initializes CommandHandler with action to execute and condition for execution.
        /// </summary>
        /// <param name="action">Action to perform when command is executing.</param>
        /// <param name="canExecute">Condition for whether command can be executed.</param>
        public CommandHandler(Action action, bool canExecute)
        {
            this.action = action;
            this.canExecute = canExecute;
        }

        /// <summary>
        /// Condition for whether command can be executed.
        /// </summary>
        public bool CanExecute(object parameter)
        {
            return this.canExecute;
        }

        /// <summary>
        /// Action to perform when command is executed.
        /// </summary>
        public void Execute(object parameter)
        {
            this.action();
        }

        /// <summary>
        /// Checks condition of CanExecute when called.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Event Handler to check if the condition of canExecute has changed.
        /// </summary>
        public event EventHandler CanExecuteChanged;
    }
}
