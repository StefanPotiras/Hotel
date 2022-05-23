using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
namespace Hotel.Helps
{
    public class RelayCommands:ICommand
    {
        private readonly Action<object> commandTask;

        public RelayCommands(Action<object> workToDo)
        {
            commandTask = workToDo;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            commandTask(parameter);
        }
    }
}
