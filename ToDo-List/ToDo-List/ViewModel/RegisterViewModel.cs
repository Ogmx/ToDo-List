using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_List.Commands;
using ToDo_List.View;

namespace ToDo_List.ViewModel
{
    class RegisterViewModel:NotificationObject
    {
        public DelegateCommands BackCommand { get; set; }
        public void Back(object parameter)
        {
            RegisterWindow win = (RegisterWindow)parameter;
            win.Close();
        }
        public RegisterViewModel()
        {
            BackCommand = new DelegateCommands();
            BackCommand.ExecuteCommand = new Action<object>(Back);
        }
    }
}
