using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ChatClient.MVVM.Core
{
    class RelayCommand : ICommand
    {
        private Action<object> execute; //把方法當引數
        private Func<object, bool> canExecute; //把方法當引數，且此方法會回傳bool


        event EventHandler ICommand.CanExecuteChanged ///當事件發生時
        {
            add
            {
                CommandManager.RequerySuggested += value; //value代表原本存在的值
            }

            remove
            {
                CommandManager.RequerySuggested += value; //value代表原本存在的值
            }
        }
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }
        public bool CanExecute(object parameter) //代表是否可以執行這個方法
        {
            return this.canExecute == null || this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            this.execute(parameter);
        }

   
    }
}
