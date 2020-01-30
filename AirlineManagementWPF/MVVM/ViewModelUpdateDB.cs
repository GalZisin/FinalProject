using AirlineManagement;
using Newtonsoft.Json;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Xml;

namespace AirlineManagementWPF
{
    public class ViewModelUpdateDB : DispatcherObject, INotifyPropertyChanged
    {

        public static List<string> countryList;
        public string urlAirlineCompanies = "";

        public Random random1 = null;
        public Random random2 = null;
        public int index = 0;

        private static Administrator[] admisitratorArray;
        private static Customer[] customersArray;
        private static string[] countriesToAddArray;
        private static string[] airlineCompaniesArray;

        private static List<string> countriesListToAdd;

        public int counterCustomers;
        public int CounterCustomers
        {
            get
            {
                return counterCustomers;
            }
            set
            {
                counterCustomers = value;
                OnPropertyChanged("CounterCustomers");
            }

        }
        public static int counterAdministrator = 0;
        public static int counterAielineCompanies = 0;
        public static int counterCountries = 0;
        public static int counterFlights = 0;
        public static int counterTickets = 0;

        private FlyingCenterSystem FCS;

        private ILoginToken _token = null;

        private LoginToken<Administrator> t = null;

        public event PropertyChangedEventHandler PropertyChanged;


        public DelegateCommand AddDBCommand { get; set; }
        public DelegateCommand ReplaceDBCommand { get; set; }

        public static int progressCounter = 0;

        public static int totalResources = 0;

        private string logMessage;
        public string LogMessage
        {
            get
            {
                return logMessage;
            }
            set
            {
                logMessage = value;
                OnPropertyChanged("LogMessage");
            }

        }
        private int numOfCustomers;
        public int NumOfCustomers
        {
            get
            {
                return numOfCustomers;
            }
            set
            {
                numOfCustomers = value;
                OnPropertyChanged("NumOfCustomers");
            }

        }
        private int numOfAirlineCompanies;
        public int NumOfAirlineCompanies
        {
            get
            {
                return numOfAirlineCompanies;
            }
            set
            {
                numOfAirlineCompanies = value;
                OnPropertyChanged("NumOfAirlineCompanies");
            }
        }
        private int numOfCountries;
        public int NumOfCountries
        {
            get
            {
                return numOfCountries;
            }
            set
            {
                numOfCountries = value;
                OnPropertyChanged("NumOfCountries");
            }
        }
        private int numOfFlights;
        public int NumOfFlights
        {
            get { return numOfFlights; }
            set
            {
                numOfFlights = value;
                OnPropertyChanged("NumOfFlights");
            }
        }
        private int numOfTickets;
        public int NumOfTickets
        {
            get { return numOfTickets; }
            set
            {
                numOfTickets = value;
                OnPropertyChanged("NumOfTickets");
            }
        }
        private int numOfAdministrators;
        public int NumOfAdministrators
        {
            get { return numOfAdministrators; }
            set
            {
                numOfAdministrators = value;
                OnPropertyChanged("NumOfAdministrators");
            }
        }
        private int _progress;
        public int Progress
        {
            get { return _progress; }
            set
            {
                _progress = value;
                OnPropertyChanged("Progress");
            }
        }
        public ViewModelUpdateDB(ILoginToken token)
        {
            _token = token;
            t = _token as LoginToken<Administrator>;
            FCS = FlyingCenterSystem.GetFlyingCenterSystemInstance();
            ILoggedInAdministratorFacade administratorFacade = FCS.GetFacade(token) as ILoggedInAdministratorFacade;

            AddDBCommand = new DelegateCommand(() => {
                Log.logger.Debug("Start AddDBCommand");
                progressCounter = 0;
                LogMessage = "";
                totalResources = NumOfCountries + NumOfCustomers + NumOfAirlineCompanies + NumOfFlights + NumOfAdministrators;
                AddToCountryList(administratorFacade, t);

                Task taskA = Task.Run(() =>
                {
                    for (int i = 0; i < NumOfAdministrators; i++)
                    {
                        InsertAdministratorsToDb(administratorFacade, t);
                    }
                    if (counterAdministrator == NumOfAdministrators)
                    {
                        if (LogMessage != "")
                        {
                            LogMessage = LogMessage + "\n";
                        }
                        LogMessage = LogMessage + string.Format("{0} Administrators created", counterAdministrator);
                    }
                    else if (counterAdministrator < NumOfAdministrators || counterAdministrator == 0)
                    {
                        if (LogMessage != "")
                        {
                            LogMessage = LogMessage + "\n";
                        }
                        LogMessage = LogMessage + string.Format($"Failed to create {NumOfCustomers} Administrators");

                    }
                    for (int i = 0; i < NumOfCustomers; i++)
                    {
                        InsertCustomerToDb(administratorFacade, t);
                    }
                    if (counterCustomers == NumOfCustomers)
                    {
                        if (LogMessage != "")
                        {
                            LogMessage = LogMessage + "\n";
                        }
                        LogMessage = LogMessage + string.Format("{0} Customers created", counterCustomers);
                    }
                    else if (counterCustomers < NumOfCustomers || counterCustomers == 0)
                    {
                        if (LogMessage != "")
                        {
                            LogMessage = LogMessage + "\n";
                        }
                        LogMessage = LogMessage + string.Format($"Failed to create {NumOfCustomers} customers");

                    }

                    InsertCountriesToDbByNumOfCountries(NumOfCountries, administratorFacade, t);
                    if (counterCountries == NumOfCountries)
                    {
                        if (LogMessage != "")
                        {
                            LogMessage = LogMessage + "\n";
                        }
                        LogMessage = LogMessage + string.Format("{0} Countries created", NumOfCountries);
                    }
                    else if (counterCountries < NumOfCountries || counterCountries == 0)
                    {
                        if (LogMessage != "")
                        {
                            LogMessage = LogMessage + "\n";
                        }
                        LogMessage = LogMessage + string.Format($"Failed to create {NumOfCountries} countries");
                    }

                    for (int i = 0; i < NumOfAirlineCompanies; i++)
                    {
                        Log.logger.Debug($"Before InsertAirlineCompanyToDb i = {i} from {NumOfAirlineCompanies}");
                        InsertAirlineCompanyToDb(administratorFacade, t);

                    }
                    if (counterAielineCompanies == NumOfAirlineCompanies)
                    {
                        if (LogMessage != "")
                        {
                            LogMessage = LogMessage + "\n";
                        }
                        LogMessage = LogMessage + string.Format("{0} Airline Companies created", NumOfAirlineCompanies);
                    }
                    else if (counterAielineCompanies < NumOfAirlineCompanies || counterAielineCompanies == 0)
                    {
                        if (LogMessage != "")
                        {
                            LogMessage = LogMessage + "\n";
                        }
                        LogMessage = LogMessage + string.Format($"Failed to create {NumOfAirlineCompanies} airline companies");
                    }

                    for (int i = 0; i < NumOfFlights; i++)
                    {
                        InsertFlightsToDb(administratorFacade, t);
                    }
                    if (counterFlights == NumOfFlights)
                    {
                        if (LogMessage != "")
                        {
                            LogMessage = LogMessage + "\n";
                        }
                        LogMessage = LogMessage + string.Format("{0} Flights created", NumOfFlights);
                    }
                    else if (counterFlights == NumOfFlights || counterFlights == 0)
                    {
                        if (LogMessage != "")
                        {
                            LogMessage = LogMessage + "\n";
                        }
                        LogMessage = LogMessage + string.Format($"Failed to create {NumOfFlights} flights");
                    }

                    for (int i = 0; i < NumOfTickets; i++)
                    {
                        InsetTicketsToDb(administratorFacade, t);
                    }
                    if (counterTickets == NumOfTickets)
                    {
                        if (LogMessage != "")
                        {
                            LogMessage = LogMessage + "\n";
                        }
                        LogMessage = LogMessage + string.Format("{0} Tickets created", NumOfTickets);
                    }
                    else if (counterTickets < NumOfTickets || counterTickets == 0)
                    {
                        if (LogMessage != "")
                        {
                            LogMessage = LogMessage + "\n";
                        }
                        LogMessage = LogMessage + string.Format($"Failed to create {NumOfTickets} tickets");
                    }

                });

            }, () => { return CanExecuteAddMethod(); });


            ReplaceDBCommand = new DelegateCommand(() =>
            {
                Log.logger.Debug("Start ReplaceDBCommand");
                InitDB.InitDataBase();
                progressCounter = 0;
                LogMessage = "";
                totalResources = NumOfCountries + NumOfCustomers + NumOfAirlineCompanies + NumOfFlights + NumOfAdministrators;
                AddToCountryList(administratorFacade, t);

                Task taskA = Task.Run(() =>
                {
                    for (int i = 0; i < NumOfAdministrators; i++)
                    {
                        InsertAdministratorsToDb(administratorFacade, t);
                    }
                    if (counterAdministrator == NumOfAdministrators)
                    {
                        if (LogMessage != "")
                        {
                            LogMessage = LogMessage + "\n";
                        }
                        LogMessage = LogMessage + string.Format("{0} Administrators created", counterAdministrator);
                    }
                    else if (counterAdministrator < NumOfAdministrators || counterAdministrator == 0)
                    {
                        if (LogMessage != "")
                        {
                            LogMessage = LogMessage + "\n";
                        }
                        LogMessage = LogMessage + string.Format($"Failed to create {NumOfCustomers} Administrators");

                    }
                    for (int i = 0; i < NumOfCustomers; i++)
                    {
                        InsertCustomerToDb(administratorFacade, t);
                    }
                    if (counterCustomers == NumOfCustomers)
                    {
                        if (LogMessage != "")
                        {
                            LogMessage = LogMessage + "\n";
                        }
                        LogMessage = LogMessage + string.Format("{0} Customers created", counterCustomers);
                    }
                    else if (counterCustomers < NumOfCustomers || counterCustomers == 0)
                    {
                        if (LogMessage != "")
                        {
                            LogMessage = LogMessage + "\n";
                        }
                        LogMessage = LogMessage + string.Format($"Failed to create {NumOfCustomers} customers");

                    }

                    InsertCountriesToDbByNumOfCountries(NumOfCountries, administratorFacade, t);
                    if (counterCountries == NumOfCountries)
                    {
                        if (LogMessage != "")
                        {
                            LogMessage = LogMessage + "\n";
                        }
                        LogMessage = LogMessage + string.Format("{0} Countries created", NumOfCountries);
                    }
                    else if (counterCountries < NumOfCountries || counterCountries == 0)
                    {
                        if (LogMessage != "")
                        {
                            LogMessage = LogMessage + "\n";
                        }
                        LogMessage = LogMessage + string.Format($"Failed to create {NumOfCountries} countries");
                    }

                    for (int i = 0; i < NumOfAirlineCompanies; i++)
                    {
                        Log.logger.Debug($"Before InsertAirlineCompanyToDb i = {i} from {NumOfAirlineCompanies}");
                        InsertAirlineCompanyToDb(administratorFacade, t);

                    }
                    if (counterAielineCompanies == NumOfAirlineCompanies)
                    {
                        if (LogMessage != "")
                        {
                            LogMessage = LogMessage + "\n";
                        }
                        LogMessage = LogMessage + string.Format("{0} Airline Companies created", NumOfAirlineCompanies);
                    }
                    else if (counterAielineCompanies < NumOfAirlineCompanies || counterAielineCompanies == 0)
                    {
                        if (LogMessage != "")
                        {
                            LogMessage = LogMessage + "\n";
                        }
                        LogMessage = LogMessage + string.Format($"Failed to create {NumOfAirlineCompanies} airline companies");
                    }

                    for (int i = 0; i < NumOfFlights; i++)
                    {
                        InsertFlightsToDb(administratorFacade, t);
                    }
                    if (counterFlights == NumOfFlights)
                    {
                        if (LogMessage != "")
                        {
                            LogMessage = LogMessage + "\n";
                        }
                        LogMessage = LogMessage + string.Format("{0} Flights created", NumOfFlights);
                    }
                    else if (counterFlights == NumOfFlights || counterFlights == 0)
                    {
                        if (LogMessage != "")
                        {
                            LogMessage = LogMessage + "\n";
                        }
                        LogMessage = LogMessage + string.Format($"Failed to create {NumOfFlights} flights");
                    }

                    for (int i = 0; i < NumOfTickets; i++)
                    {
                        InsetTicketsToDb(administratorFacade, t);
                    }
                    if (counterTickets == NumOfTickets)
                    {
                        if (LogMessage != "")
                        {
                            LogMessage = LogMessage + "\n";
                        }
                        LogMessage = LogMessage + string.Format("{0} Tickets created", NumOfTickets);
                    }
                    else if (counterTickets < NumOfTickets)
                    {
                        if (LogMessage != "")
                        {
                            LogMessage = LogMessage + "\n";
                        }
                        LogMessage = LogMessage + string.Format($"Half a failure created {counterTickets} /{NumOfTickets} tickets");
                    }
                    else if (counterTickets == 0)
                    {
                        if (LogMessage != "")
                        {
                            LogMessage = LogMessage + "\n";
                        }
                        LogMessage = LogMessage + string.Format($"Failed to create {NumOfTickets} tickets");
                    }
                });


            }, () => { return CanExecuteAddMethod(); });


            Task.Run(() =>
            {
                while (true)
                {
                    AddDBCommand.RaiseCanExecuteChanged(); // go check the enable/disable
                    ReplaceDBCommand.RaiseCanExecuteChanged(); // go check the enable/disable
                    Thread.Sleep(250);
                }

            });

        }

        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        private void SafeInvoke(Action work)
        {
            if (Dispatcher.CheckAccess()) // CheckAccess returns true if you're on the dispatcher thread
            {
                work.Invoke();
                return;
            }
            this.Dispatcher.BeginInvoke(work);
        }
        public void InsertAdministratorsToDb(ILoggedInAdministratorFacade administratorFacade, LoginToken<Administrator> t)
        {
            int counter = 0;
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\JSON\MOCK_DATA.json";
            string json = File.ReadAllText(path);
            List<RootObject> playerList = JsonConvert.DeserializeObject<List<RootObject>>(json);
            admisitratorArray = new Administrator[playerList.Count];
            Administrator newAdministrator = null;

            for (int i = 0; i < admisitratorArray.Length; i++)
            {
                newAdministrator = new Administrator();
                newAdministrator.FIRST_NAME = playerList[i].FirstName;
                newAdministrator.LAST_NAME = playerList[i].LastName;
                newAdministrator.USER_NAME = playerList[i].UserName;
                newAdministrator.PASSWORD = playerList[i].Password;
                admisitratorArray[i] = newAdministrator;
            }
            counter = 0;
            while (true)
            {
                random1 = new Random();
                index = random1.Next(0, admisitratorArray.Length);
                Administrator administrator = administratorFacade.GetAdministratorByUserName(t, admisitratorArray[index].USER_NAME);
                if (administrator == null)
                {
                    administratorFacade.CreateNewAdministrator(t, admisitratorArray[index]);
                    progressCounter++;
                    Progress = 100 * progressCounter / totalResources;
                    counterAdministrator++;
                    break;
                }
                counter++;
                if (counter > admisitratorArray.Length)
                {
                    break;
                }
            }
        }
        public void InsertCustomerToDb(ILoggedInAdministratorFacade administratorFacade, LoginToken<Administrator> t)
        {
            int counter = 0;
            string path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + @"\JSON\MOCK_DATA.json";
            string json = File.ReadAllText(path);
            List<RootObject> playerList = JsonConvert.DeserializeObject<List<RootObject>>(json);
            customersArray = new Customer[playerList.Count];
            Customer newCustomer;
            for (int i = 0; i < customersArray.Length; i++)
            {
                newCustomer = new Customer();
                newCustomer.FIRST_NAME = playerList[i].FirstName;
                newCustomer.LAST_NAME = playerList[i].LastName;
                newCustomer.USER_NAME = playerList[i].UserName;
                newCustomer.PASSWORD = playerList[i].Password;
                newCustomer.ADDRESS = playerList[i].Address;
                newCustomer.PHONE_NO = playerList[i].PhoneNumber;
                newCustomer.CREDIT_CARD_NUMBER = playerList[i].CreditCardNumber;
                customersArray[i] = newCustomer;
            }
            counter = 0;
            while (true)
            {
                random1 = new Random();
                index = random1.Next(0, customersArray.Length);
                Customer customer = administratorFacade.GetCustomerByUserName(t, customersArray[index].USER_NAME);
                if (customer == null)
                {
                    administratorFacade.CreateNewCustomer(t, customersArray[index]);
                    progressCounter++;
                    Progress = 100 * progressCounter / totalResources;
                    CounterCustomers++;
                    break;
                }
                counter++;
                if (counter > customersArray.Length)
                {
                    break;
                }
            }
        }

        public async Task<string> WriteWebRequestCountriesAsync(string url)
        {
            string text = "";
            try
            {
                WebRequest webRequest = WebRequest.Create(url);
                webRequest.Timeout = 4000;
                WebResponse response = await webRequest.GetResponseAsync();

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    text = await reader.ReadToEndAsync();
                }
            }
            catch
            {
                Log.logger.Debug("url " + url + " not responce");
            }

            return text;
        }

        private void InsertAirlineCompanyToDb(ILoggedInAdministratorFacade administratorFacade, LoginToken<Administrator> t)
        {
            int count1 = 0;
            IList<Country> countries = null;
            Country country = null;
            AirlineCompany newAirlineCompany = null;
            AirlineCompany airlineCompany = null;
            bool isAirlineCompanyInserted = false;
            int count2 = 0;
            while (!isAirlineCompanyInserted && count2 < 20)
            {
                if(NumOfCountries == 0)
                {
                    random1 = new Random();
                    countries = administratorFacade.GetAllCountries(t);
                    string[] newCountryArray = new string[countries.Count];

                    for (int i = 0; i < countries.Count; i++)
                    {
                        newCountryArray[i] = countries[i].COUNTRY_NAME;
                    }

                    index = random1.Next(0, newCountryArray.Length);
                    country = administratorFacade.GetCountryByName(t, newCountryArray[index]);
                }
                else
                {
                    random1 = new Random();
                    index = random1.Next(0, countriesToAddArray.Length);
                    country = administratorFacade.GetCountryByName(t, countriesToAddArray[index]);
                }
               

                string randomAirlineCompanyName = "";
                try
                {
                    randomAirlineCompanyName = GetRandomAirlineCompanyName(country.COUNTRY_NAME, administratorFacade, t);
                }
                catch { Log.logger.Debug(" GetRandomAirlineCompanyName not return value"); }
                if (randomAirlineCompanyName == null || randomAirlineCompanyName == "")
                {
                    Log.logger.Debug("randomAirlineCompanyName == blank -continue");
                    count2++;
                    continue;
                }
                randomAirlineCompanyName = randomAirlineCompanyName.Replace("/", "");
                airlineCompany = administratorFacade.GetAirlineCompanyByAirlineName(t, randomAirlineCompanyName);
                if (airlineCompany == null)
                {
                    string randomString = "";
                    newAirlineCompany = new AirlineCompany();
                    newAirlineCompany.AIRLINE_NAME = randomAirlineCompanyName;
                    newAirlineCompany.COUNTRY_CODE = country.ID;
                    count1 = 0;
                    while (true)
                    {
                        Customer customer = administratorFacade.GetCustomerByUserName(t, RandomString());
                        if (customer == null)
                        {
                            randomString = RandomString();
                            newAirlineCompany.USER_NAME = randomString;
                            break;
                        }
                        count1++;
                        if (count1 > 10)
                        {
                            break;
                        }
                    }

                    newAirlineCompany.PASSWORD = RandomString();
                    administratorFacade.CreateNewAirline(t, newAirlineCompany);
                    progressCounter++;
                    Progress = 100 * progressCounter / totalResources;
                    counterAielineCompanies++;
                    isAirlineCompanyInserted = true;
                }
                count2++;
                if (count2 > 19)
                {
                    Log.logger.Debug("Counter > 19");
                }
            }


        }
        public string GetRandomAirlineCompanyName(string country, ILoggedInAdministratorFacade administratorFacade, LoginToken<Administrator> t)
        {
            AirlineCompany airlineCompany = null;
            int counter = 0;
            bool airlineCompanyExist = true;
            Thread.Sleep(2000);
            string airlineCompanyName = "";
            Log.logger.Debug("GetRandomAirlineCompanyName" + " country=" + country);
            urlAirlineCompanies = "https://en.wikipedia.org/wiki/List_of_airlines_of_Russia".Replace("Russia", country);
            Log.logger.Debug("urlAirlineCompanies = " + urlAirlineCompanies);
            Task<string> task = WriteWebRequestCountriesAsync(urlAirlineCompanies);
            string airlineCompaniesHtml = task.Result;

            string[] paragraphArray = airlineCompaniesHtml.Split('\n');
            List<string> ls = new List<string>();
            for (int j = 0; j < paragraphArray.Length; j++)
            {
                if (paragraphArray[j] == "<tr>")
                {
                    XmlDocument doc = new XmlDocument();
                    try
                    {
                        if (paragraphArray[j + 1].Substring(paragraphArray[j + 1].Length - 5, 5) != "</td>")
                            paragraphArray[j + 1] = paragraphArray[j + 1] + "</td>";
                        doc.LoadXml(paragraphArray[j + 1]);
                    }
                    catch
                    {
                        Log.logger.Debug("Not found continue"); continue;
                    }

                    string name = "";
                    try
                    {
                        name = doc.ChildNodes[0].ChildNodes[0].ChildNodes[0].Value;
                    }
                    catch { name = doc.ChildNodes[0].Value; }
                    Log.logger.Debug("add name " + name);
                    ls.Add(name);
                }
            }
            if (ls.Count == 0)
            {
                return "";
            }
            airlineCompaniesArray = new string[ls.Count];
            for (int i = 0; i < ls.Count; i++)
            {
                airlineCompaniesArray[i] = ls[i];
            }
            counter = 0;
            airlineCompanyExist = true;
            while (airlineCompanyExist && counter < airlineCompaniesArray.Length + 10)
            {
                random1 = new Random();
                index = random1.Next(0, airlineCompaniesArray.Length);
                airlineCompany = administratorFacade.GetAirlineCompanyByAirlineName(t, airlineCompaniesArray[index]);
                if (airlineCompany == null)
                {
                    airlineCompanyName = airlineCompaniesArray[index];
                    airlineCompanyExist = false;
                    break;
                }
                counter++;
            }

            return airlineCompanyName;
        }
        public void AddToCountryList(ILoggedInAdministratorFacade administratorFacade, LoginToken<Administrator> t)
        {
            Country country = null;
            countryList = new List<string>();
            country = new Country();
            for (int i = 0; i < CountryArray.Names.Length - 1; i++)
            {

                country = administratorFacade.GetCountryByName(t, CountryArray.Names[i]);

                if (country == null)
                {
                    countryList.Add(CountryArray.Names[i]);
                }
            }
        }

        public void InsertCountriesToDbByNumOfCountries(int NumOfCountries, ILoggedInAdministratorFacade administratorFacade, LoginToken<Administrator> t)
        {
            Country newCountry = null;
            string countryName = "";
            string[] countriesNotInDbArray = new string[countryList.Count];
            countriesToAddArray = new string[NumOfCountries];
            countriesListToAdd = new List<string>();
            for (int i = 0; i < countriesNotInDbArray.Length - 1; i++)
            {
                countriesNotInDbArray[i] = countryList[i];
                countriesListToAdd.Add(countryList[i]);
            }


            for (int i = 0; i < NumOfCountries; i++)
            {
                for (int j = 1; j <= 10; j++)
                {

                    random1 = new Random();
                    index = random1.Next(0, countriesNotInDbArray.Length);
                    urlAirlineCompanies = "https://en.wikipedia.org/wiki/List_of_airlines_of_Russia".Replace("Russia", countriesNotInDbArray[index]);
                    Task<string> task = WriteWebRequestCountriesAsync(urlAirlineCompanies);
                    string res = task.Result;
                    if (res == "")
                    {
                        continue;
                    }
                    else
                    {
                        countryName = countriesNotInDbArray[index];
                        break;
                    }
                }

                countriesToAddArray[i] = countryName;
                newCountry = new Country();
                newCountry.COUNTRY_NAME = countriesToAddArray[i];
                Country country = administratorFacade.GetCountryByName(t, newCountry.COUNTRY_NAME);
                if (country == null)
                {
                    administratorFacade.CreateNewCountry(t, newCountry);
                    countriesListToAdd.Remove(countryName);
                    countriesNotInDbArray = new string[countriesListToAdd.Count];
                    for (int k = 0; k < countriesListToAdd.Count; k++)
                    {
                        countriesNotInDbArray[k] = countriesListToAdd[k];

                    }
                    progressCounter++;
                    Progress = 100 * progressCounter / totalResources;
                    counterCountries++;
                }
            }
        }

        public void InsertFlightsToDb(ILoggedInAdministratorFacade administratorFacade, LoginToken<Administrator> t)
        {
            
            Flight newFlight = null;
            bool equalsOroginCountry = true;
            IList<AirlineCompany> airlineCompaniesListFromDb = new List<AirlineCompany>();
            IList<Country> countries = new List<Country>();
            airlineCompaniesListFromDb = administratorFacade.GetAllAirlineCompanies(t);
            string[] airlineCompaniesArrayFromDb = new string[airlineCompaniesListFromDb.Count];
            for (int i = 0; i < airlineCompaniesArrayFromDb.Length; i++)
            {
                airlineCompaniesArrayFromDb[i] = airlineCompaniesListFromDb[i].AIRLINE_NAME;
            }
            random1 = new Random();
            index = random1.Next(0, airlineCompaniesArrayFromDb.Length);
            AirlineCompany airlineCompany = administratorFacade.GetAirlineCompanyByAirlineName(t, airlineCompaniesArrayFromDb[index]);
            newFlight = new Flight();
            while (equalsOroginCountry)
            {
                random1 = new Random();
                if (NumOfCountries == 0)
                {
                    countries = administratorFacade.GetAllCountries(t);
                    string[] newCountryArray = new string[countries.Count];
                  
                    for (int i = 0; i < countries.Count; i++)
                    {
                        newCountryArray[i] = countries[i].COUNTRY_NAME;
                    }

                    index = random1.Next(0, newCountryArray.Length);
                    Country destinationCountry = administratorFacade.GetCountryByName(t, newCountryArray[index]);
                    if (destinationCountry.ID != airlineCompany.COUNTRY_CODE)
                    {
                        newFlight.DESTINATION_COUNTRY_CODE = destinationCountry.ID;
                        equalsOroginCountry = false;
                        break;
                    }
                }
                else
                {
                    index = random1.Next(0, countriesToAddArray.Length);
                    Country destinationCountry = administratorFacade.GetCountryByName(t, countriesToAddArray[index]);
                    if (destinationCountry.ID != airlineCompany.COUNTRY_CODE)
                    {
                        newFlight.DESTINATION_COUNTRY_CODE = destinationCountry.ID;
                        equalsOroginCountry = false;
                        break;
                    }
                }
            }
            newFlight.AIRLINECOMPANY_ID = airlineCompany.ID;
            newFlight.ORIGIN_COUNTRY_CODE = airlineCompany.COUNTRY_CODE;
            newFlight.DEPARTURE_TIME = RandomDepartureDate();
            newFlight.LANDING_TIME = RandomLandingDate();
            newFlight.REMANING_TICKETS = 300;
            newFlight.TOTAL_TICKETS = 300;
            administratorFacade.CreateFlight(t, newFlight);
            progressCounter++;
            Progress = 100 * progressCounter / totalResources;
            counterFlights++;
        }
        public void InsetTicketsToDb(ILoggedInAdministratorFacade administratorFacade, LoginToken<Administrator> t)
        {
            int count = 0;
            int index1 = 0;
            int index2 = 0;
            IList<Customer> customerList = administratorFacade.GetAllCustomers(t);
            Customer[] customerArray = new Customer[customerList.Count];
            for (int i = 0; i < customerArray.Length; i++)
            {
                customerArray[i] = customerList[i];
            }
            IList<Flight> flightsList = administratorFacade.GetAllFlights(t);
            Flight[] flightsArray = new Flight[flightsList.Count];
            for (int i = 0; i < flightsArray.Length; i++)
            {
                flightsArray[i] = flightsList[i];
            }
            count = 0;
            while (true)
            {
                random1 = new Random();
                random2 = new Random();
                index1 = random1.Next(0, customerArray.Length);
                index2 = random2.Next(0, flightsArray.Length);
                Ticket ticket = administratorFacade.GetTicketByCustomerId(t, customerArray[index1].ID);
                if ((ticket == null || ticket.FLIGHT_ID != flightsArray[index2].ID) && !(flightsArray[index2].REMANING_TICKETS <= 0))
                {
                    administratorFacade.AddTicket(t, customerArray[index1].ID, flightsArray[index2].ID);
                    progressCounter++;
                    if(totalResources == 0)
                    {
                        totalResources = 1;
                    }
                    Progress = 100 * progressCounter / totalResources;
                    counterTickets++;
                    break;
                }
                count++;
                if (count > 10)
                {
                    break;
                }
            }

        }
        //public DateTime RandomDepartureDate()
        //{
        //    DateTime startDeparture;
        //    Random gen;
        //    int range;
        //    startDeparture = new DateTime(2020, 5, 30);
        //    gen = new Random();
        //    range = (startDeparture - DateTime.Today).Days;

        //    return startDeparture.AddDays(gen.Next(range)).AddHours(gen.Next(0, 24)).AddMinutes(gen.Next(0, 60)).AddSeconds(gen.Next(0, 60));
        //}
        //public DateTime RandomLandingDate()
        //{
        //    DateTime startLanding;
        //    Random gen;
        //    int range;
        //    startLanding = new DateTime(2020, 5, 30);
        //    gen = new Random();
        //    range = (startLanding - DateTime.Today).Days;

        //    return startLanding.AddDays(gen.Next(range + 20)).AddHours(gen.Next(0, 24)).AddMinutes(gen.Next(0, 60)).AddSeconds(gen.Next(0, 60));
        //}
        public DateTime RandomDepartureDate()
        {
            double minutes = 0;
            DateTime start = DateTime.Now;
            Random rnd = new Random();
            minutes = rnd.Next(1, 721);

            DateTime currentTime = DateTime.Now;
          
           return currentTime.AddMinutes(minutes);
        }
        public DateTime RandomLandingDate()
        {
            double minutes = 0;
            DateTime start = DateTime.Now;
            Random rnd = new Random();
            minutes = rnd.Next(1, 721);

            DateTime currentTime = DateTime.Now;

            return currentTime.AddMinutes(minutes);
        }
        private static Random random = new Random();
        public static string RandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 10)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private bool CanExecuteAddMethod()
        {
            if (progressCounter >= totalResources)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    public class RootObject
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string CreditCardNumber { get; set; }
    }

}
