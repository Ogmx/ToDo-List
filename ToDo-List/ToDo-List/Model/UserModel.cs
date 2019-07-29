using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_List.ViewModel;

//定义User类
namespace ToDo_List.Model
{
    public class User
    {
        public int ID {get; set;}
        public string Name { get; set; }
        public int Age { get; set; }
        public string Sex { get; set; }
        public string Remarks { get; set; }
    }

}
