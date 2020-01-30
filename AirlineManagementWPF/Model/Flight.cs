//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace AirlineManagementWPF
//{
//    public class Flight : INotifyPropertyChanged
//    {
//        private long id;
//        public long ID
//        {
//            get
//            {
//                return this.id;
//            }
//            set
//            {
//                this.id = value;
//                OnPropertyChanged("ID");
//            }
//        }
//        public long airlineCompany_Id;
//        public long AIRLINECOMPANY_ID
//        {
//            get
//            {
//                return this.airlineCompany_Id;
//            }
//            set
//            {
//                this.airlineCompany_Id = value;
//                OnPropertyChanged("AIRLINECOMPANY_ID");
//            }
//        }
//        private long originCountryCode;
//        public long ORIGIN_COUNTRY_CODE
//        {
//            get
//            {
//                return this.originCountryCode;
//            }
//            set
//            {
//                this.originCountryCode = value;
//                OnPropertyChanged("ORIGIN_COUNTRY_CODE");
//            }
//        }
//        private long detinationCountryCode;
//        public long DESTINATION_COUNTRY_CODE
//        {
//            get
//            {
//                return this.detinationCountryCode;
//            }
//            set
//            {
//                this.detinationCountryCode = value;
//                OnPropertyChanged("DESTINATION_COUNTRY_CODE");
//            }
//        }
//        private DateTime departureTime;
//        public DateTime DEPARTURE_TIME
//        {
//            get
//            {
//                return this.departureTime;
//            }
//            set
//            {
//                this.departureTime = value;
//                OnPropertyChanged("DEPARTURE_TIME");
//            }
//        }
//        private DateTime LandingTime;
//        public DateTime LANDING_TIME
//        {
//            get
//            {
//                return this.LandingTime;
//            }
//            set
//            {
//                this.LandingTime = value;
//                OnPropertyChanged("LANDING_TIME");
//            }
//        }
//        private int remainingTickets;
//        public int REMANING_TICKETS
//        {
//            get
//            {
//                return this.remainingTickets;
//            }
//            set
//            {
//                this.remainingTickets = value;
//                OnPropertyChanged("REMANING_TICKETS");
//            }
//        }
//        private int totalTickets;
//        public int TOTAL_TICKETS
//        {
//            get
//            {
//                return this.totalTickets;
//            }
//            set
//            {
//                this.totalTickets = value;
//                OnPropertyChanged("TOTAL_TICKETS");
//            }
//        }
//        public event PropertyChangedEventHandler PropertyChanged;
//        public override string ToString()
//        {
//            return $"Flight ID {ID} AIRLINECOMPANY_ID {AIRLINECOMPANY_ID} ORIGIN_COUNTRY_CODE {ORIGIN_COUNTRY_CODE} DESTINATION_COUNTRY_CODE {DESTINATION_COUNTRY_CODE} DEPARTURE_TIME {DEPARTURE_TIME} LANDING_TIME {LANDING_TIME} REMANING_TICKETS {REMANING_TICKETS} TOTAL_TICKETS {TOTAL_TICKETS}";
//        }

//        private void OnPropertyChanged(string property)
//        {
//            if (PropertyChanged != null)
//            {
//                PropertyChanged(this, new PropertyChangedEventArgs(property));
//            }
//        }
//    }
//}
