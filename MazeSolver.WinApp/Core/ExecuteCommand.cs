using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MazeSolver.WinApp.Core
{
    public class ExecuteCommand : ICommand
    {
        private readonly Action<object> _actionToExecute;
        private readonly Predicate<object> _ableToExecute;

        public ExecuteCommand(Action<object> actionToExecute, Predicate<object> ableToExecute)
        {
            if (actionToExecute == null)
            {
                throw new Exception("Error: Action to execute is null.");
            }              

            _actionToExecute = actionToExecute;
            _ableToExecute = ableToExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _ableToExecute == null || _ableToExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _actionToExecute(parameter);
        }
    }
}
