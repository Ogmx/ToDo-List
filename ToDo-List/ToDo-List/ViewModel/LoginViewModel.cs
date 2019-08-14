using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;
using ToDo_List.Commands;
using ToDo_List.Model;
using ToDo_List.Services;
using ToDo_List.View;

namespace ToDo_List.ViewModel
{
    class LoginViewModel:NotificationObject
    {
        string _name=string.Empty;
        private string password;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }
        public DelegateCommands LoginCommand { get; set; }
        public DelegateCommands GoToRegisterCommand { get; set; }
        public void Login(object parameter)
        {
            LoginWindow win = (LoginWindow)parameter;
            SQLiteDataService ds = new SQLiteDataService();
            string _password = ds.GetPWD(Name);
            if(_password==null)
            {
                MessageBox.Show("该账户不存在");
                return;
            }
            password = win.passwordBox.Password;
            if(password != _password)
            {
                MessageBox.Show("密码错误");
                return;
            }
            MessageBox.Show("登录成功");
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
