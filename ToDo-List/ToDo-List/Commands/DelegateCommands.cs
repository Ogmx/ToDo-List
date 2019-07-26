using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ToDo_List.Commands
{
    public class DelegateCommands : ICommand
    {
        public Action<object> ExecuteCommand = null;

        public Func<object, bool> CanExecuteCommand = null;

        //当命令可执行状态发生改变时，应当被激发
        public event EventHandler CanExecuteChanged;

        //用于判断命令是否可以执行
        public bool CanExecute(object parameter)
        {
            if (CanExecuteCommand != null)
            {
                return this.CanExecuteCommand(parameter);
            }
            else
            {
                return true;
            }
        }
        //命令执行
        public void Execute(object parameter)
        {
            if (this.ExecuteCommand != null) this.ExecuteCommand(parameter);
        }

    }
}
