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
using AirlineManagement;

namespace AirlineManagementWPF
{
    /// <summary>
    /// Interaction logic for AirlineList.xaml
    /// </summary>
    public partial class FlightsList : Window
    {
        private FlyingCenterSystem FCS;
        private ILoginToken _token;
        private LoginToken<AirlineCompany> t = null;
        public FlightsList(ILoginToken token)
        {
            InitializeComponent();
            _token = token;
            t = _token as LoginToken<AirlineCompany>;
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            //ILoggedInCustomer airlineFacade = FCS.GetFacade(_token) as ILoggedInCustomer;
            
            //FlightsComboBox.ItemsSource = airlineFacade.GetAllFlights(t);
            FlightsComboBox.SelectedItem = null;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FindFlight findFlight= new FindFlight(t);
            findFlight.ShowDialog();

            // Hide the MainWindow until later
            //this.Hide();
        }
    }
    
}
