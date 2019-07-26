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

namespace ToDo_List.ViewModel
{
    public class MainViewModel:NotificationObject
    {
        ObservableCollection<UserModel> _mylist = new ObservableCollection<UserModel>();

        string _name = string.Empty;
        int _id = 0;
        int _age = 0;
        string _sex = string.Empty;
        string _remarks = string.Empty;

        //定义数据操作
        public ObservableCollection<UserModel> mylist   
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

        //定义功能操作
        public void addStudent(object parameter)
        {
            int id = mylist[mylist.Count - 1].ID;
            mylist.Add(new UserModel() { ID = id + 1, Name = Name, Age = Age, Sex = Sex, Remarks = Remarks });
            //Binding();
            RaisePropertyChanged("myList");
        }
        public void updateStudent(object parameter)
        {
            if (ID == 0)
            {
                MessageBox.Show("请选择修改项");
                return;
            }
            foreach (var item in _mylist)
            {
                if (item.ID == ID)
                {
                    item.ID = ID;
                    item.Name = Name;
                    item.Sex = Sex;
                    item.Remarks = Remarks;
                    item.Age = Age;
                    break;
                }
            }
            //Binding();
            mylist = _mylist;

        }
        public void deleteStudent(object parameter)
        {
            if (ID == 0)
            {
                MessageBox.Show("请选择删除项");
                return;
            }
            UserModel user1 = _mylist.Single(p => p.ID == ID);
            _mylist.Remove(user1);
            mylist = _mylist;

        }
        public void selectUser(object parameter)
        {

            if (parameter != null)
            {
                DataGrid dg = (DataGrid)parameter;
                if (Type.GetType("ShowDataViewModel") == dg.SelectedItems[0].GetType())
                {
                    UserModel user = (UserModel)dg.SelectedItems[0];
                    ID = user.ID;
                    Name = user.Name;
                    Age = user.Age;
                    Sex = user.Sex;
                    Remarks = user.Remarks;
                }

            }
        }


        //定义构造函数，关联功能操作与DelegateCommands
        public MainViewModel()
        {

            AddCommand = new DelegateCommands();
            AddCommand.ExecuteCommand = new Action<object>(addStudent);

            UpdateCommand = new DelegateCommands();
            UpdateCommand.ExecuteCommand = new Action<object>(updateStudent);//修改方法

            DeleteCommand = new DelegateCommands();
            DeleteCommand.ExecuteCommand = new Action<object>(deleteStudent);//修改方法

            SelectionChangedCommand = new DelegateCommands();
            SelectionChangedCommand.ExecuteCommand = new Action<object>(selectUser);

            //模拟数据
            mylist.Add(new Model.UserModel() { ID = 1, Name = "张三", Age = 20, Sex = "女", Remarks = "无" });
            mylist.Add(new Model.UserModel() { ID = 2, Name = "李四", Age = 21, Sex = "女", Remarks = "无" });
            mylist.Add(new Model.UserModel() { ID = 3, Name = "王五", Age = 22, Sex = "女", Remarks = "无" });
            mylist.Add(new Model.UserModel() { ID = 4, Name = "赵六", Age = 24, Sex = "女", Remarks = "无" });
        }

        public DelegateCommands AddCommand { get; set; }
        public DelegateCommands UpdateCommand { get; set; }
        public DelegateCommands DeleteCommand { get; set; }
        public DelegateCommands SelectionChangedCommand { get; set; }

       
    }
}
