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
            if (Name.Length > 18)
            {
                MessageBox.Show("姓名过长！");
                return;
            }

            if (Age > 100 || Age < 0)
            {
                MessageBox.Show("年龄输入有误！");
                return;
            }

            if (Sex.Length == 0)
            {
                MessageBox.Show("请选择性别");
                return;
            }

            if (Remarks.Length > 3)
            {
                MessageBox.Show("备注过长！");
                return;
            }

           

            if (list[0].Name == null )
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
            if (list[0].Age <= 0 ||list[0].Age>=150 || list[0].Age.GetType() == list[0].Name.GetType())
            {
                MessageBox.Show("请输入正确年龄");
                return;
            }
           
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
