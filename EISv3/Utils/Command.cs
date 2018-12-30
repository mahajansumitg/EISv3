using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace EISv3.Utils
{
    public class Command : ICommand
    {
        Action<object> executeAction;
        Func<object, bool> canExecute;

        public Command(Action<object> executeAction)
        {
            this.executeAction = executeAction;
        }
        public Command(Action<object> executeAction, Func<object, bool> canExecute)
        {
            this.executeAction = executeAction;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return canExecute == null ? true : canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter)
        {
            executeAction(parameter);
        }
    }
}
