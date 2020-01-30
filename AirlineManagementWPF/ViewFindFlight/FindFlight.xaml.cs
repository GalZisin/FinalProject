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
    /// Interaction logic for FindFlight.xaml
    /// </summary>
    public partial class FindFlight : Window
    {
        private FlyingCenterSystem FCS;
        private ILoginToken _token;
        private LoginToken<AirlineCompany> t = null;
        //private MainWindowViewModel mvvml;
       // private MainWindowViewModel viewModel = new MainWindowViewModel();
        public FindFlight(ILoginToken token)
        {
            InitializeComponent();
            _token = token;
            t = _token as LoginToken<AirlineCompany>;
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
           
            //DataContext = viewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //ILoggedInCustomer airlineFacade = FCS.GetFacade(_token) as ILoggedInCustomer;
            long flightId = 0;
            AirlineManagement.Flight flight = null;
            try
            {
                flightId = Convert.ToInt32(FlightIdTxtBox.Text);
                //flight = airlineFacade.GetFlightByFlightId(t, flightId);
            }
            catch (Exception e1)
            {
                MessageBox.Show("Flight id must be numerical");
                return;
            }
            
            if (flight == null)
            {
                MessageBox.Show("Flight id does not found");
                return;
            }
            //viewModel.MyFlight.ID = flight.ID;
            //viewModel.MyFlight.AIRLINECOMPANY_ID = flight.AIRLINECOMPANY_ID;
            //viewModel.MyFlight.ORIGIN_COUNTRY_CODE = flight.ORIGIN_COUNTRY_CODE;
            //viewModel.MyFlight.DESTINATION_COUNTRY_CODE = flight.DESTINATION_COUNTRY_CODE;
            //viewModel.MyFlight.DEPARTURE_TIME = flight.DEPARTURE_TIME;
            //viewModel.MyFlight.LANDING_TIME = flight.LANDING_TIME;
            //viewModel.MyFlight.REMANING_TICKETS = flight.REMANING_TICKETS;
            //viewModel.MyFlight.TOTAL_TICKETS = flight.TOTAL_TICKETS;
        }
    }
}
