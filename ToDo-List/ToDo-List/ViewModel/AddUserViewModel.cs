using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ToDo_List.Commands;
using ToDo_List.Model;
using ToDo_List.Services;
using ToDo_List.View;

namespace ToDo_List.ViewModel
{
    class AddUserViewModel: NotificationObject
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

        string _name = string.Empty;
        int _age = 0;
        string _sex = string.Empty;
        string _remarks = "无";

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
        public DelegateCommands AddCommand { get; set; }

        public void Back(object parameter)
        {
            AddUserWindow win = (AddUserWindow)parameter;
            if(win!=null)
                win.Close();
        }
        public void addStudent(object parameter)
        {
            WriteLog("进行了添加操作");
            SQLiteDataService ds = new SQLiteDataService();
            List<User> list = new List<User>();
            list.Add(new User() { ID = 0, Name = Name, Age = Age, Sex = Sex, Remarks = Remarks });
            ds.InsertUser(list);
            MessageBox.Show("添加成功");
            AddUserWindow win = (AddUserWindow)parameter;
            if (win != null)
                win.Close();
        }

        public AddUserViewModel()
        {
            BackCommand = new DelegateCommands();
            BackCommand.ExecuteCommand = new Action<object>(Back);
            AddCommand = new DelegateCommands();
            AddCommand.ExecuteCommand = new Action<object>(addStudent);
        }
    }
}
