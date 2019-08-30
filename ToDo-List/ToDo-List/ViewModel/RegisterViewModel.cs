using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToDo_List.Commands;
using ToDo_List.View;
using ToDo_List.Services;
using System.Text.RegularExpressions;

namespace ToDo_List.ViewModel
{
    class RegisterViewModel:NotificationObject
    {
        string _name=null;
        string _password=null;
        string _password2=null;

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged("Password");
            }
        }
        public string Password2
        {
            get { return _password2; }
            set
            {
                _password2 = value;
                RaisePropertyChanged("Password2");
            }
        }

        public DelegateCommands BackCommand { get; set; }
        public DelegateCommands RegisterCommand { get; set; }

        public void Back(object parameter)
        {
            RegisterWindow win = (RegisterWindow)parameter;
            win.Close();
        }
        public void Register(object parameter)
        {

            if (Name==null)
            {
                MessageBox.Show("用户名不能为空");
                return;
            }
            if(Password==null)
            {
                MessageBox.Show("密码不能为空");
                return;
            }
            Regex reg = new Regex(@"[^0-9]"); // 排除型字符组(取反思想)
            if (reg.IsMatch(Name))
            {
                MessageBox.Show("姓名中不能包含特殊符号！");
                return;
            }

            if (reg.IsMatch(Password) || reg.IsMatch(Password2))
            {
                MessageBox.Show("密码中不能包含特殊符号！");
                return;
            }
            if (Password!=Password2)
            {
                MessageBox.Show("两次输入的密码不一致");
                return;
            }
            SQLiteDataService ds = new SQLiteDataService();
            if (ds.Register(Name, Password))
            {
                MessageBox.Show("注册成功");
                RegisterWindow win = (RegisterWindow)parameter;
                win.Close();
            }
            else
            {
                MessageBox.Show("注册失败，该用户名已存在");
                return;
            }
                

        }
        public RegisterViewModel()
        {
            BackCommand = new DelegateCommands();
            BackCommand.ExecuteCommand = new Action<object>(Back);
            RegisterCommand = new DelegateCommands();
            RegisterCommand.ExecuteCommand = new Action<object>(Register);
        }
    }
}
