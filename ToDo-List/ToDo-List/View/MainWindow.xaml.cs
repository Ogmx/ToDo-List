using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToDo_List.View;
using ToDo_List.ViewModel;

namespace ToDo_List
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            LoginWindow login = new LoginWindow();
            login.ShowDialog();
            if (login.DialogResult == true)
            {
                login.Close();
                InitializeComponent();
                this.DataContext = new MainViewModel();
            }
            else
            {
                login.Close();
                this.Close();
            }
                
            
        }
    }
}
