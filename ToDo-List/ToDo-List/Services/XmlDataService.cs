using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
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
        public int GetNum()
        {
            string xmlFileName = System.IO.Path.Combine(Environment.CurrentDirectory, @"Data\UserInfo.xml");
            XDocument xDOc = XDocument.Load(xmlFileName);
            var users = xDOc.Descendants("User");
            return users.Count();
        }
        public void SetAllUsers(ObservableCollection<User>UserList)
        {
            string xmlFileName = System.IO.Path.Combine(Environment.CurrentDirectory, @"Data\UserInfo.xml");    
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

        public void SetUser(List<User>UserList)
        {
            string xmlFileName = System.IO.Path.Combine(Environment.CurrentDirectory, @"Data\UserInfo.xml");
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(xmlFileName); 
            XmlNode root = xDoc.SelectSingleNode("Users");
            foreach (var d in UserList)
            {
                XmlElement user = xDoc.CreateElement("User");
                XmlElement a1 = xDoc.CreateElement("ID");
                a1.InnerText = d.ID.ToString();
                user.AppendChild(a1);
                XmlElement a2 = xDoc.CreateElement("Name");
                a2.InnerText = d.Name;
                user.AppendChild(a2);
                XmlElement a3 = xDoc.CreateElement("Age");
                a3.InnerText = d.Age.ToString();
                user.AppendChild(a3);
                XmlElement a4 = xDoc.CreateElement("Sex");
                a4.InnerText = d.Sex;
                user.AppendChild(a4);
                XmlElement a5 = xDoc.CreateElement("Remarks");
                a5.InnerText = d.Remarks;
                user.AppendChild(a5);

                root.AppendChild(user);
            }
            xDoc.Save(xmlFileName);
        }
    }
}
