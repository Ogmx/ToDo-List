using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using ToDo_List.Model;
using System.Data.SQLite;
using System.Configuration;
using System.Data;

namespace ToDo_List.Services
{
    class SQLiteDataService
    {

        //private static string connStr = ConfigurationManager.ConnectionStrings["UserInfoDB"].ConnectionString;
        SQLiteConnection cn;
        SQLiteCommand cmd;
        
      //创建一个连接到指定数据库
        void ConnectToDatabase()
        {
            cn = new SQLiteConnection("Data Source=Data/UserInfo.db;Version=3;");
            cn.Open();
            cmd = new SQLiteCommand();
            cmd.Connection = cn;
            
        }
        void ConnectToAccountInfo()
        {
            cn = new SQLiteConnection("Data Source=Data/AccountInfo.db;Version=3;");
            cn.Open();
            cmd = new SQLiteCommand();
            cmd.Connection = cn;
        }
   
      //在指定数据库中创建一个table
        void CreateTable()
        {
            ConnectToDatabase();
            cmd.CommandText = "CREATE TABLE IF NOT EXISTS User(id int,name varchar(20),age int,sex varchar(4),remark varchar(50))";//输入SQL语句
            cmd.ExecuteNonQuery();//调用此方法运行
            cn.Close();
        }
        
        public void initTable()
        {
            CreateTable();
            cmd.CommandText = "INSERT INTO User VALUES(1,'张三',20,'男','无')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO User VALUES(2,'李四',31,'男','无')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO User VALUES(3,'王五',26,'女','无')";
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        int Count()
        {
            ConnectToDatabase();
            int cnt = 0;
            cmd.CommandText = "SELECT count(*) FROM User";
            SQLiteDataReader sr = cmd.ExecuteReader();
            sr.Read();
            cnt = sr.GetInt32(0);
            sr.Close();
            //cn.Close();
            return cnt;
        }

        public void InsertUser(List<User>list)
        {
            ConnectToDatabase();
            int id = Count()+1;
            string name = list[0].Name;
            int age = list[0].Age;
            string sex = list[0].Sex;
            string remark = list[0].Remarks;
            cmd.CommandText = "INSERT INTO User(id,name,age,sex,remark) VALUES(@id,@name,@age,@sex,@remark)";
            cmd.Parameters.Add("id", DbType.Int32).Value = id;
            cmd.Parameters.Add("name", DbType.String).Value = name;
            cmd.Parameters.Add("age", DbType.Int32).Value = age;
            cmd.Parameters.Add("sex", DbType.String).Value = sex;
            cmd.Parameters.Add("remark", DbType.String).Value = remark;
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public void DeleteUser(int _id)
        {
            ConnectToDatabase();
            cmd.CommandText = "DELETE FROM User WHERE id=@id";
            cmd.Parameters.Add("id", DbType.Int32).Value = _id;
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public void UpdateUser(List<User>list)
        {
            ConnectToDatabase();
            int id = list[0].ID;
            string name = list[0].Name;
            int age = list[0].Age;
            string sex = list[0].Sex;
            string remark = list[0].Remarks;
            cmd.CommandText = "UPDATE User SET id=@id,name=@name,age=@age,sex=@sex,remark=@remark where id=@id";
            cmd.Parameters.Add("id", DbType.Int32).Value = id;
            cmd.Parameters.Add("name", DbType.String).Value = name;
            cmd.Parameters.Add("age", DbType.Int32).Value = age;
            cmd.Parameters.Add("sex", DbType.String).Value = sex;
            cmd.Parameters.Add("remark", DbType.String).Value = remark;
            cmd.ExecuteNonQuery();
            cn.Close();
        }

        public int FindUser(string name)
        {
            int id = 0;
            ConnectToDatabase();
            cmd.CommandText = "SELECT * FROM User WHERE name=@name";
            cmd.Parameters.Add("name", DbType.String).Value = name;
            SQLiteDataReader sr = cmd.ExecuteReader();
            if(sr.Read())
                id = sr.GetInt32(0);
            sr.Close();
            cn.Close();
            return id;
        }

        public string GetPWD(string name)
        {
            string ans = null;
            ConnectToAccountInfo();
            cmd.CommandText = "SELECT * FROM Account WHERE name=@name";
            cmd.Parameters.Add("name", DbType.String).Value = name;
            SQLiteDataReader sr = cmd.ExecuteReader();
            if (sr.Read())
                ans = sr.GetString(2);
            sr.Close();
            cn.Close();
            return ans;
        }

        public bool Register(string name,string password)
        {
            ConnectToAccountInfo();
            cmd.CommandText = "SELECT * FROM Account WHERE name=@name";
            cmd.Parameters.Add("name", DbType.String).Value = name;
            SQLiteDataReader sr = cmd.ExecuteReader();
            if (sr.Read())
            {
                sr.Close();
                cn.Close();
                return false;
            }
            sr.Close();
            cmd.CommandText = "INSERT INTO Account(name,password) VALUES(@name,@password)";
            cmd.Parameters.Add("name", DbType.String).Value = name;
            cmd.Parameters.Add("password", DbType.String).Value = password;
            cmd.ExecuteNonQuery();
            cn.Close();
            return true;
        }
        public ObservableCollection<User> GetAllUsers()
        {
            ObservableCollection<User> mylist = new ObservableCollection<User>();
            ConnectToDatabase();
            cmd.CommandText = "SELECT * FROM User";
            SQLiteDataReader sr = cmd.ExecuteReader();
            while(sr.Read())
            {
                User user = new User();
                user.ID = sr.GetInt32(0);
                user.Name = sr.GetString(1);
                user.Age = sr.GetInt32(2);
                user.Sex = sr.GetString(3);
                user.Remarks = sr.GetString(4);
                mylist.Add(user);
            }
            sr.Close();
            cn.Close();
            return mylist;
        }

    }
}
