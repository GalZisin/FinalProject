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
    /// Interaction logic for BuyTicket.xaml
    /// </summary>
    public partial class BuyTicket : Window
    {
        private ViewModelFlight vm; 
        private static ILoginToken _token =null;
        
        public BuyTicket(ILoginToken token)
        {
            _token = token;
            vm = new ViewModelFlight(_token);
            InitializeComponent();

            this.DataContext = vm;

            myFlights.ItemsSource = vm.allFlights;
        }
        
    }
}
