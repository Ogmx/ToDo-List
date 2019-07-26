using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ToDo_List.ViewModel
{
    public class UserViewModel:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged(string Name)
        {
            if(Name != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(Name));
            }
        }
    }
}
