using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_List.Commands;
using ToDo_List.View;

namespace ToDo_List.ViewModel
{
    class LoginViewModel:NotificationObject
    {
        public DelegateCommands LoginCommand { get; set; }
        public DelegateCommands GoToRegisterCommand { get; set; }
        public void Login(object parameter)
        {
            LoginWindow win = (LoginWindow)parameter;
            win.DialogResult = true;
            win.Close();
        }
        public void GoToRegister(object parameter)
        {
            RegisterWindow win = new RegisterWindow();
            win.ShowDialog();
        }
        public LoginViewModel()
        {
            LoginCommand = new DelegateCommands();
            LoginCommand.ExecuteCommand = new Action<object>(Login);
            GoToRegisterCommand = new DelegateCommands();
            GoToRegisterCommand.ExecuteCommand = new Action<object>(GoToRegister);
        }
    }
}
