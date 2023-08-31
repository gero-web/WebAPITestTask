using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CLientTestTask.ViewModel.ComadsBase
{
    public class CommandBase : ICommand
    {

        private readonly Action<object> execute;
        private readonly Func<object, bool> canExecute;


        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public CommandBase(Action<object> action, Func<object, bool> canExcute = null )
        {
            this.execute = action;
            this.canExecute = canExcute;
        }

        public bool CanExecute(object parameter)
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
           this.execute(parameter); ;
        }
    }
}
