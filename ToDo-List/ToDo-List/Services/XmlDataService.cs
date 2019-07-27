using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ToDo_List.Model;

namespace ToDo_List.Services
{
    class XmlDataService:IDataService
    {
        public ObservableCollection<User> GetAllUsers()
        {
            ObservableCollection<User> UserList = new ObservableCollection<User>();
            string xmlFileName = System.IO.Path.Combine(Environment.CurrentDirectory, @"Data\UserInfo.xml");
            XDocument xDOc = XDocument.Load(xmlFileName);
            var users = xDOc.Descendants("User");
            foreach(var d in users)
            {
                User user = new User();
                user.ID = int.Parse(d.Element("ID").Value);
                user.Name = d.Element("Name").Value;
                user.Age = int.Parse(d.Element("Age").Value);
                user.Sex = d.Element("Sex").Value;
                user.Remarks = d.Element("Remarks").Value;
                UserList.Add(user);
            }
            return UserList;
        }

        public void SetAllUsers(ObservableCollection<User>UserList)
        {
            string xmlFileName = System.IO.Path.Combine(Environment.CurrentDirectory, @"Data\Test.xml");
            XElement root = new XElement("Users");
            
            foreach(var d in UserList)
            {
                XElement user = new XElement("User");
                user.SetElementValue("ID", d.ID);
                user.SetElementValue("Name", d.Name);
                user.SetElementValue("Age", d.Age);
                user.SetElementValue("Sex", d.Sex);
                user.SetElementValue("Remarks", d.Remarks);
                root.Add(user);
            }
            root.Save(xmlFileName);
        }
    }
}
