using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo_List.Model;

namespace ToDo_List.Services
{
    interface IDataService
    {
        List<User> GetAllUsers();
    }
}
