using AirlineManagement;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace DBGenerator
{
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


    class Program
    {
        //public static Customer[] customersArray;

        ////public static string[] airlineCompaniesArray;
        //public static string urlAirlineCompanies = "";
        //public static Random random = null;
        //public static int index = 0;
        //public static string airlineCompany;

        //public static async Task<string> WriteWebRequestCountriesAsync(string url)
        //{
        //    string text;
        //    WebRequest webRequest = WebRequest.Create(url);

        //    WebResponse response = await webRequest.GetResponseAsync();

        //    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
        //    {
        //        text = await reader.ReadToEndAsync();


        //    }
        //    return text;
        //}
        //public static string CreateRandomAirlineCompany(string country)
        //{

        //    urlAirlineCompanies = "https://en.wikipedia.org/wiki/List_of_airlines_of_Russia".Replace("Russia", country);

        //    Task<string> task = WriteWebRequestCountriesAsync(urlAirlineCompanies);

        //    string airlineCompaniesHtml = task.Result;

        //    string[] paragraphArray = airlineCompaniesHtml.Split('\n');

        //    List<string> ls = new List<string>();

        //    for (int j = 0; j < paragraphArray.Length - 1; j++)
        //    {

        //        if (paragraphArray[j] == "<tr>")
        //        {

        //            XmlDocument doc = new XmlDocument();
        //            doc.LoadXml(paragraphArray[j + 1]);
        //            try
        //            {
        //                string name = doc.ChildNodes[0].ChildNodes[0].ChildNodes[0].Value;

        //                ls.Add(name);
        //            }
        //            catch { }


        //        }

        //    }
        //    string[] airlineCompaniesArray = new string[ls.Count];
        //    for (int i = 0; i < ls.Count; i++)
        //    {
        //        airlineCompaniesArray[i] = ls[i];
        //    }
        //    for (int i = 0; i < airlineCompaniesArray.Length; i++)
        //    {
        //        random = new Random();
        //        index = random.Next(0, airlineCompaniesArray.Length);
        //    }


        //    return airlineCompany;
        //}
        //private static Random random = new Random();
        //public static string RandomString()
        //{
        //    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        //    return new string(Enumerable.Repeat(chars, 10)
        //      .Select(s => s[random.Next(s.Length)]).ToArray());
        //}
        static void Main(string[] args)
        {
            //Random random = new Random();
            //for (int i = 0; i < 30; i++)
            //{

            //    int randomMinutes = random.Next(5, 21);
            //    Console.WriteLine(randomMinutes);
            //}
            int[] arr= RandomNumber(5,20);
            for (int i = 0; i < 15; i++)
            {
                Console.WriteLine(arr[i]);
            }
           
            //Console.WriteLine(Names[52]);
            //Console.WriteLine(Names[53]);
            //Console.WriteLine(Names[54]);

            //for(int i=0; i<Names.Length - 1; i++)
            //{
            //    Console.WriteLine($"index: {i}, name: {Names[i]}");
            //}

            //string[] ac = new string[50];
            ////urlAirlineCompanies = "https://en.wikipedia.org/wiki/List_of_airlines_of_Russia".Replace("Russia", "Russia");

            ////Task<string> task = WriteWebRequestCountriesAsync(urlAirlineCompanies);
            ////ac = CreateAirlineCompaniesArray("Russia");

            //foreach (string a in ac)
            //{
            //    Console.WriteLine(a);
            //}
            //RandomDate randomDate = new RandomDate();

            //for (int i = 0; i < 20; i++)
            //{
            //    Console.WriteLine(randomDate.DepartureDate().ToString("yyyy-MM-dd HH:mm"));
            //}
            //Console.WriteLine();
            //Console.WriteLine();
            //for (int i = 0; i < 20; i++)
            //{
            //    Console.WriteLine(randomDate.LandingDate().ToString("yyyy-MM-dd HH:mm"));
            //}
            //for (int i = 0; i < 20; i++)
            //{
            //    Console.WriteLine(RandomString());
            //}
            //string json = File.ReadAllText("C:\\Users\\GAL\\Desktop\\Dot_net\\DotNetCourse\\OOP\\Main_Project\\MainProjectWithWPF\\AirlineManagement\\AirlineManagementWPF\\JSON\\MOCK_DATA.json");
            //List<RootObject> playerList = JsonConvert.DeserializeObject<List<RootObject>>(json);
            //RootObject rootObj = JsonConvert.DeserializeObject<RootObject>(json);
            //foreach(RootObject root in playerList)
            //{
            //    Console.WriteLine(root.FirstName.ToString());
            //}
            //customersArray = new Customer[playerList.Count];
            //Customer newCustomer;
            //for (int i = 0; i < customersArray.Length; i++)
            //{
            //    newCustomer = new Customer();
            //    newCustomer.FIRST_NAME = playerList[i].FirstName;
            //    newCustomer.LAST_NAME = playerList[i].LastName;
            //    newCustomer.USER_NAME = playerList[i].UserName;
            //    newCustomer.PASSWORD = playerList[i].Password;
            //    newCustomer.ADDRESS = playerList[i].Address;
            //    newCustomer.PHONE_NO = playerList[i].PhoneNumber;
            //    newCustomer.CREDIT_CARD_NUMBER = playerList[i].CreditCardNumber;
            //    customersArray[i] = newCustomer;
            //}

            //for (int i = 0; i < 100; i++)
            //{
            //    Console.WriteLine($"{ customersArray[i].FIRST_NAME}, { customersArray[i].LAST_NAME}, {customersArray[i].USER_NAME}, {customersArray[i].PASSWORD}, {  customersArray[i].ADDRESS},{ customersArray[i].PHONE_NO}, { customersArray[i].CREDIT_CARD_NUMBER}");
            //}
            //    double minutes = 0;

            //    DateTime currentTime = DateTime.Now;
            //    Console.WriteLine(currentTime);
            //    Console.WriteLine();
            //    Console.WriteLine();
            //    for (int i = 0; i < 20; i++)
            //    {
            //        minutes = DepartureDate();

            //        Console.WriteLine(currentTime.AddMinutes(minutes).ToString());
            //        Thread.Sleep(100);
            //    }
            //    Console.WriteLine();
            //    Console.WriteLine();

            //    for (int i = 0; i < 100; i++)
            //    {
            //        minutes = DepartureDate();

            //        Console.WriteLine(currentTime.AddMinutes(-minutes).ToString());
            //        Thread.Sleep(100);
            //    }

            Console.ReadLine();
            //}
            //public static double DepartureDate()
            //{
            //    DateTime start = DateTime.Now;
            //    Random rnd = new Random();
            //    return rnd.Next(1, 721);
            //}
            //     public static string[] Names = new string[]
            //{
            //     "Afghanistan",
            //     "Albania",
            //     "Algeria",
            //     "American Samoa",
            //     "Andorra",
            //     "Angola",
            //     "Anguilla",
            //     "Antarctica",
            //     "Antigua and Barbuda",
            //     "Argentina",
            //     "Armenia",
            //     "Aruba",
            //     "Australia",
            //     "Austria",
            //     "Azerbaijan",
            //     "Bahamas",
            //     "Bahrain",
            //     "Bangladesh",
            //     "Barbados",
            //     "Belarus",
            //     "Belgium",
            //     "Belize",
            //     "Benin",
            //     "Bermuda",
            //     "Bhutan",
            //     "Bolivia",
            //     "Bosnia and Herzegovina",
            //     "Botswana",
            //     "Bouvet Island",
            //     "Brazil",
            //     "British Indian Ocean Territory",
            //     "Brunei Darussalam",
            //     "Bulgaria",
            //     "Burkina Faso",
            //     "Burundi",
            //     "Cambodia",
            //     "Cameroon",
            //     "Canada",
            //     "Cape Verde",
            //     "Cayman Islands",
            //     "Central African Republic",
            //     "Chad",
            //     "Chile",
            //     "China",
            //     "Christmas Island",
            //     "Cocos (Keeling) Islands",
            //     "Colombia",
            //     "Comoros",
            //     "Congo",
            //     "Congo, the Democratic Republic of the",
            //     "Cook Islands",
            //     "Costa Rica",
            //     "Cote D'Ivoire",
            //     "Croatia",
            //     "Cuba",
            //     "Cyprus",
            //     "Czech Republic",
            //     "Denmark",
            //     "Djibouti",
            //     "Dominica",
            //     "Dominican Republic",
            //     "Ecuador",
            //     "Egypt",
            //     "El Salvador",
            //     "Equatorial Guinea",
            //     "Eritrea",
            //     "Estonia",
            //     "Ethiopia",
            //     "Falkland Islands (Malvinas)",
            //     "Faroe Islands",
            //     "Fiji",
            //     "Finland",
            //     "France",
            //     "French Guiana",
            //     "French Polynesia",
            //     "French Southern Territories",
            //     "Gabon",
            //     "Gambia",
            //     "Georgia",
            //     "Germany",
            //     "Ghana",
            //     "Gibraltar",
            //     "Greece",
            //     "Greenland",
            //     "Grenada",
            //     "Guadeloupe",
            //     "Guam",
            //     "Guatemala",
            //     "Guinea",
            //     "Guinea-Bissau",
            //     "Guyana",
            //     "Haiti",
            //     "Heard Island and Mcdonald Islands",
            //     "Holy See (Vatican City State)",
            //     "Honduras",
            //     "Hong Kong",
            //     "Hungary",
            //     "Iceland",
            //     "India",
            //     "Indonesia",
            //     "Iran, Islamic Republic of",
            //     "Iraq",
            //     "Ireland",
            //     "Israel",
            //     "Italy",
            //     "Jamaica",
            //     "Japan",
            //     "Jordan",
            //     "Kazakhstan",
            //     "Kenya",
            //     "Kiribati",
            //     "Korea, Democratic People's Republic of",
            //     "Korea, Republic of",
            //     "Kuwait",
            //     "Kyrgyzstan",
            //     "Lao People's Democratic Republic",
            //     "Latvia",
            //     "Lebanon",
            //     "Lesotho",
            //     "Liberia",
            //     "Libyan Arab Jamahiriya",
            //     "Liechtenstein",
            //     "Lithuania",
            //     "Luxembourg",
            //     "Macao",
            //     "Macedonia, the Former Yugoslav Republic of",
            //     "Madagascar",
            //     "Malawi",
            //     "Malaysia",
            //     "Maldives",
            //     "Mali",
            //     "Malta",
            //     "Marshall Islands",
            //     "Martinique",
            //     "Mauritania",
            //     "Mauritius",
            //     "Mayotte",
            //     "Mexico",
            //     "Micronesia, Federated States of",
            //     "Moldova, Republic of",
            //     "Monaco",
            //     "Mongolia",
            //     "Montserrat",
            //     "Morocco",
            //     "Mozambique",
            //     "Myanmar",
            //     "Namibia",
            //     "Nauru",
            //     "Nepal",
            //     "Netherlands",
            //     "Netherlands Antilles",
            //     "New Caledonia",
            //     "New Zealand",
            //     "Nicaragua",
            //     "Niger",
            //     "Nigeria",
            //     "Niue",
            //     "Norfolk Island",
            //     "Northern Mariana Islands",
            //     "Norway",
            //     "Oman",
            //     "Pakistan",
            //     "Palau",
            //     "Palestinian Territory, Occupied",
            //     "Panama",
            //     "Papua New Guinea",
            //     "Paraguay",
            //     "Peru",
            //     "Philippines",
            //     "Pitcairn",
            //     "Poland",
            //     "Portugal",
            //     "Puerto Rico",
            //     "Qatar",
            //     "Reunion",
            //     "Romania",
            //     "Russian Federation",
            //     "Rwanda",
            //     "Saint Helena",
            //     "Saint Kitts and Nevis",
            //     "Saint Lucia",
            //     "Saint Pierre and Miquelon",
            //     "Saint Vincent and the Grenadines",
            //     "Samoa",
            //     "San Marino",
            //     "Sao Tome and Principe",
            //     "Saudi Arabia",
            //     "Senegal",
            //     "Serbia and Montenegro",
            //     "Seychelles",
            //     "Sierra Leone",
            //     "Singapore",
            //     "Slovakia",
            //     "Slovenia",
            //     "Solomon Islands",
            //     "Somalia",
            //     "South Africa",
            //     "South Georgia and the South Sandwich Islands",
            //     "Spain",
            //     "Sri Lanka",
            //     "Sudan",
            //     "Suriname",
            //     "Svalbard and Jan Mayen",
            //     "Swaziland",
            //     "Sweden",
            //     "Switzerland",
            //     "Syrian Arab Republic",
            //     "Taiwan, Province of China",
            //     "Tajikistan",
            //     "Tanzania, United Republic of",
            //     "Thailand",
            //     "Timor-Leste",
            //     "Togo",
            //     "Tokelau",
            //     "Tonga",
            //     "Trinidad and Tobago",
            //     "Tunisia",
            //     "Turkey",
            //     "Turkmenistan",
            //     "Turks and Caicos Islands",
            //     "Tuvalu",
            //     "Uganda",
            //     "Ukraine",
            //     "United Arab Emirates",
            //     "United Kingdom",
            //     "United States",
            //     "United States Minor Outlying Islands",
            //     "Uruguay",
            //     "Uzbekistan",
            //     "Vanuatu",
            //     "Venezuela",
            //     "Viet Nam",
            //     "Virgin Islands, British",
            //     "Virgin Islands, US",
            //     "Wallis and Futuna",
            //     "Western Sahara",
            //     "Yemen",
            //     "Zambia",
            //     "Zimbabwe",
            //};

        }

        private static int[] RandomNumber(int from, int rangeEx)
        {
            var orderedList = Enumerable.Range(from, rangeEx);
            var rng = new Random();
            return orderedList.OrderBy(c => rng.Next()).ToArray();
        }
    }
}

//customersArray[i].LAST_NAME = playerList[i].LastName;
//                customersArray[i].USER_NAME = playerList[i].UserName;
//                customersArray[i].PASSWORD = playerList[i].Password;
//                customersArray[i].ADDRESS = playerList[i].Address;
//                customersArray[i].PHONE_NO = playerList[i].PhoneNumber;
//                customersArray[i].CREDIT_CARD_NUMBER = playerList[i].CreditCardNumber;