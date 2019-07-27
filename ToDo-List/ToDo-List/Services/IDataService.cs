using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_List.Model;

namespace ToDo_List.Services
{
    interface IDataService
    {
        ObservableCollection<User> GetAllUsers();
    }
}
