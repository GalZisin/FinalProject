using AirlineManagement;
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
using System.Windows.Shapes;

namespace AirlineManagementWPF
{
    /// <summary>
    /// Interaction logic for DBUpdate.xaml
    /// </summary>
    public partial class DBUpdate : Window
    {
        private ViewModelUpdateDB vm;
        private static ILoginToken _token = null;
        public DBUpdate(ILoginToken token)
        {
            _token = token;
           
            InitializeComponent();
            vm = new ViewModelUpdateDB(_token);
            this.DataContext = vm;
        }
    }
}
