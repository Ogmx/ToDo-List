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
    class FindUserViewModel:NotificationObject
    {
        public static ILog logError = LogManager.GetLogger("ErrorLog");
        public static ILog logInfor = LogManager.GetLogger("InforLog");
        /// 记录错误日志
        public static void WriteLog(string infor, Exception ex)
        {
            if (logError.IsErrorEnabled)
            {
                logError.Error(infor, ex);
            }
        }
        /// 记录普通日志
        public static void WriteLog(string infor)
        {
            if (logInfor.IsInfoEnabled)
            {
                logInfor.Info(infor);
            }
        }

        string _name = string.Empty;
        private List<User> mylist = new List<User>();

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged("Name");
            }
        }

        public DelegateCommands BackCommand { get; set; }
        public DelegateCommands SearchCommand { get; set; }

        public void Back(object parameter)
        {
            FindUserWindow win = (FindUserWindow)parameter;
            win.Close();
        }
        public void searchStudent(object parameter)
        {
            WriteLog("进行了查找操作");
            if (Name.Length != 0)
            {
                SQLiteDataService ds = new SQLiteDataService();
                int id = ds.FindUser(Name);
                if (id == 0)
                    MessageBox.Show("未找到此人");
                else
                    MessageBox.Show("查找成功，id="+id);
            }
            else
            {
                MessageBox.Show("请填写要查找的姓名");
            }

        }
        public FindUserViewModel()
        {
            BackCommand = new DelegateCommands();
            BackCommand.ExecuteCommand = new Action<object>(Back);
            SearchCommand = new DelegateCommands();
            SearchCommand.ExecuteCommand = new Action<object>(searchStudent);
        }
    }
}
