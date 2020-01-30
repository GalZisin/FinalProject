using AirlineManagement;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AirlineManagementWPF
{
    class ViewModelFlight: INotifyPropertyChanged
    {
        public ObservableCollection<Flight> allFlights = new ObservableCollection<Flight>();
        public DelegateCommand BuyCommand { get; set; }

        public DelegateCommand SearchFlightCommand { get; set; }
        public DelegateCommand ShowFlightsCommand { get; set; }

        public long FlightNumber { get; set; }

        public Flight MyFlight { get; set; }

        private FlyingCenterSystem FCS;
        private ILoginToken _token=null;
        private LoginToken<Customer> t = null;

        public event PropertyChangedEventHandler PropertyChanged;
        public ViewModelFlight(ILoginToken token)
        {
            FlightNumber = 0;
            _token = token;
            t = _token as LoginToken<Customer>;
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInCustomerFacade customerFacade = FCS.GetFacade(token) as ILoggedInCustomerFacade;

            ShowFlightsCommand = new DelegateCommand(() =>
            {
                allFlights.AddRange(customerFacade.GetAllFlights(t));

            }, () => { return true; });


            //SearchFlightCommand = new DelegateCommand(() => {
            //    MyFlight = customerFacade.GetFlightByFlightId(t, FlightNumber);

            //    PropertyChanged(this, new PropertyChangedEventArgs("MyFlight"));

            //    BuyCommand.RaiseCanExecuteChanged();
            //}, () => { return true; });

            allFlights = new ObservableCollection<Flight>();


            BuyCommand = new DelegateCommand(() => {
                MessageBox.Show("Buying ticket! yeah!");
            }, () => { return MyFlight != null && MyFlight.REMANING_TICKETS > 0; });

            SearchFlightCommand = new DelegateCommand(() =>
            {
                MyFlight = customerFacade.GetFlightByFlightId(t, FlightNumber);
                OnPropertyChanged("MyFlight");

                BuyCommand.RaiseCanExecuteChanged();

            },() => { return true; });

        }
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
