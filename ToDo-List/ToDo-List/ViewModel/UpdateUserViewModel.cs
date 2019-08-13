using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using ToDo_List.Commands;
using ToDo_List.Model;
using ToDo_List.Services;
using ToDo_List.View;

namespace ToDo_List.ViewModel
{
    class UpdateUserViewModel: NotificationObject
    {
        public static ILog logError = LogManager.GetLogger("ErrorLog");
        public static ILog logInfor = LogManager.GetLogger("InforLog");
        public static void WriteLog(string infor, Exception ex)
        {
            if (logError.IsErrorEnabled)
            {
                logError.Error(infor, ex);
            }
        }/// 记录错误日志
        public static void WriteLog(string infor)
        {
            if (logInfor.IsInfoEnabled)
            {
                logInfor.Info(infor);
            }
        }/// 记录普通日志

        public static int _id;
        string _name = string.Empty;
        int _age = 0;
        string _sex = string.Empty;
        string _remarks = "无";

        public int ID
        {
            get { return _id; }
            set
            {
                _id = value;
                RaisePropertyChanged("ID");
            }
        }
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }
        public string Sex
        {
            get { return _sex; }
            set
            {
                _sex = value;
                RaisePropertyChanged("Sex");
            }
        }
        public string Remarks
        {
            get { return _remarks; }
            set
            {
                _remarks = value;
                RaisePropertyChanged("Remarks");
            }
        }
        public int Age
        {
            get { return _age; }
            set
            {
                _age = value;
                RaisePropertyChanged("Age");
            }
        }

        public DelegateCommands BackCommand { get; set; }
        public DelegateCommands UpdateCommand { get; set; }

        public void Back(object parameter)
        {
            UpdateUserWindow win = (UpdateUserWindow)parameter;
            if (win != null)
                win.Close();
        }
        public void updateStudent(object parameter)
        {
            WriteLog("进行了修改操作");
            List<User> list = new List<User>();
            SQLiteDataService ds = new SQLiteDataService();
            list.Add(new User() { ID = ID, Name = Name, Age = Age, Sex = Sex, Remarks = Remarks });
            if (list[0].Name == null)
            {
                MessageBox.Show("姓名不能为空");
                return;
            }
            Regex reg = new Regex(@"[^0-9]"); // 排除型字符组(取反思想)
            if (!reg.IsMatch(list[0].Name))
            {
                MessageBox.Show("请输入正确姓名");
                return;
            }
            if (list[0].Age <= 0 || list[0].Age >= 150 || list[0].Age.GetType() == list[0].Name.GetType())
            {
                MessageBox.Show("请输入正确年龄");
                return;
            }
            if (list[0].Sex == null)
            {
                MessageBox.Show("请选择性别");
                return;
            }
            ds.UpdateUser(list);
            MessageBox.Show("修改成功");
            UpdateUserWindow win = (UpdateUserWindow)parameter;
            if (win != null)
                win.Close();
        }

        public UpdateUserViewModel()
        {
            BackCommand = new DelegateCommands();
            BackCommand.ExecuteCommand = new Action<object>(Back);
            UpdateCommand = new DelegateCommands();
            UpdateCommand.ExecuteCommand = new Action<object>(updateStudent);//修改方法
        }
    }
}
