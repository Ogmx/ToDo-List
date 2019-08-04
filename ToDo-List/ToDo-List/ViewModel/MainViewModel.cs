using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ToDo_List.Commands;
using ToDo_List.Model;
using ToDo_List.Services;
using ToDo_List.View;
using log4net;

namespace ToDo_List.ViewModel
{

    public class MainViewModel : NotificationObject
    {
        //日志操作
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


        ObservableCollection<User> _mylist = new ObservableCollection<User>();


        string _name = string.Empty;
        int _id = 0;
        int _age = 0;
        string _sex = string.Empty;
        string _remarks = string.Empty;


        //定义数据属性
        public ObservableCollection<User> mylist
        {
            get { return _mylist; }
            set
            {
                _mylist = value;
                RaisePropertyChanged("mylist");
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
        public int ID
        {
            get { return _id; }
            set
            {
                _id = value;
                RaisePropertyChanged("ID");
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
   
     
       
        //声明命令属性
        public DelegateCommands UpdateCommand { get; set; }
        public DelegateCommands DeleteCommand { get; set; }
        public DelegateCommands SelectionChangedCommand { get; set; }
        public DelegateCommands SaveCommand { get; set; }
        public DelegateCommands JumpToAddUserCommand { get; set; }
        public DelegateCommands JumpToFindUserCommand { get; set; }
        //定义命令属性
        public void updateStudent(object parameter)
        {
            if (ID == 0)
            {
                MessageBox.Show("请选择修改项");
                return;
            }
            else
            {
                WriteLog("进行了修改操作" );
                mylist[ID - 1] = new User() { ID = ID, Name = Name, Age = Age, Sex = Sex, Remarks = Remarks };
                MessageBox.Show("修改成功");
            }
        }
        public void deleteStudent(object parameter)
        {
            if (ID == 0)
            {
                MessageBox.Show("请选择删除项");
                return;
            }
            WriteLog("进行了删除操作");
            User user1 = _mylist.Single(p => p.ID == ID);
            _mylist.Remove(user1);
            mylist = _mylist;
            ID = 0;

        }
        public void selectUser(object parameter)
        {
            WriteLog("进行了选中操作");
            if (parameter != null)
            {
                DataGrid dg = (DataGrid)parameter;
                if (dg.SelectedItems.Count > 0)
                {
                    User user = (User)dg.SelectedItems[0];
                    ID = user.ID;
                    Name = user.Name;
                    Age = user.Age;
                    Sex = user.Sex;
                    Remarks = user.Remarks;
                }

            }
        }
        public void SaveUserInfo()
        {
            WriteLog("进行了保存操作");
            XmlDataService ds = new XmlDataService();
            ds.SetAllUsers(mylist);
            MessageBox.Show("保存成功");
        }
        public void JumpToFindUser(object parameter)
        {
            FindUserWindow a = new FindUserWindow();
            a.Show();
        }
        public void JumpToAddUser(object parameter)
        {
            AddUserWindow a = new AddUserWindow();
            a.ShowDialog();
            LoadUserInfo();
        }
        //关联命令属性
        public MainViewModel()
        {

            UpdateCommand = new DelegateCommands();
            UpdateCommand.ExecuteCommand = new Action<object>(updateStudent);//修改方法

            DeleteCommand = new DelegateCommands();
            DeleteCommand.ExecuteCommand = new Action<object>(deleteStudent);//删除方法

            SelectionChangedCommand = new DelegateCommands();
            SelectionChangedCommand.ExecuteCommand = new Action<object>(selectUser);

            JumpToAddUserCommand = new DelegateCommands();
            JumpToAddUserCommand.ExecuteCommand = new Action<object>(JumpToAddUser);

            JumpToFindUserCommand = new DelegateCommands();
            JumpToFindUserCommand.ExecuteCommand = new Action<object>(JumpToFindUser);

            LoadUserInfo();
        }

        public void LoadUserInfo()
        {
            mylist.Clear();
            XmlDataService ds = new XmlDataService();
            var users = ds.GetAllUsers();
            
            foreach(var user in users)
            {
                mylist.Add(new User() { ID = user.ID, Name = user.Name, Age = user.Age, Sex = user.Sex, Remarks = user.Remarks });
            }
          
        }
    }
}
