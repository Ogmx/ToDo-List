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

        /// <summary>
        /// 记录错误日志
        /// </summary>
        public static void WriteLog(string infor, Exception ex)
        {
            if (logError.IsErrorEnabled)
            {
                logError.Error(infor, ex);
            }
        }
        /// <summary>
        /// 记录普通日志
        /// </summary>
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
            int selectid = 0;
            if (Name.Length != 0)
            {
                bool bFind = mylist.Any<User>(p => p.Name == Name);
                if (bFind)
                {

                    //dataGrid
                    long find = 0;
                    long len = mylist.LongCount();
                    for (long i = 0; i < len; ++i)
                    {
                        if (mylist[(int)i].Name == Name)
                        {
                            find = i;
                            break;
                        }
                    }
                    selectid = (int)find;
                    MessageBox.Show("查找成功，编号为：" + (selectid + 1));
                }
                else
                {
                    MessageBox.Show("查找失败");
                }
            }
            else
            {
                MessageBox.Show("请填写要查找的姓名");
            }

        }
        public void LoadUserInfo()
        {
            XmlDataService ds = new XmlDataService();
            var users = ds.GetAllUsers();

            foreach (var user in users)
            {
                mylist.Add(new User() { ID = user.ID, Name = user.Name, Age = user.Age, Sex = user.Sex, Remarks = user.Remarks });
            }

        }

        public FindUserViewModel()
        {
            BackCommand = new DelegateCommands();
            BackCommand.ExecuteCommand = new Action<object>(Back);
            SearchCommand = new DelegateCommands();
            SearchCommand.ExecuteCommand = new Action<object>(searchStudent);

            LoadUserInfo();
        }
    }
}
