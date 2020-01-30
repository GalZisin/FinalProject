using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement
{
    public class TestResource
    {
        //AdministratorFacade : UserNmae, Password
        public const string Administrator_USER_NAME = "admin";
        public const string Administrator_PASSWORD = "9999";

        //AdministratorFacade: Wrong Password
        public const string Administrator_AdministratorPasswordNotFound_PASSWORD = "8888";

        //Customer1 Data
        public const string Customer1_FIRST_NAME = "Bar";
        public const string Customer1_LAST_NAME = "BoBi";
        public const string Customer1_USER_NAME = "Bar12345";
        public const string Customer1_PASSWORD = "1234";
        public const string Customer1_ADDRESS = "Or Akiva";
        public const string Customer1_PHONE_NO = "0505468844";
        public const string Customer1_CREDIT_CARD_NUMBER = "37237238";

        //Customer2 Data
        public const string Customer2_FIRST_NAME = "Yael";
        public const string Customer2_LAST_NAME = "Levi";
        public const string Customer2_USER_NAME = "Yael12345";
        public const string Customer2_PASSWORD = "12345";
        public const string Customer2_ADDRESS = "Rehovot";
        public const string Customer2_PHONE_NO = "0505348857";
        public const string Customer2_CREDIT_CARD_NUMBER = "37231138";

        ////CustomerFacade:
        //public const string CustomerFacade_COUNTRY_NAME2 = "Russia";

        //CustomerFacade: Customer Wrong Password
        public const string CustomerWrongPassword_PASSWORD = "1234567";

        //AirlineCompany1 Data
        public const string AirlineCompany1_AIRLINE_NAME = "El-AL";
        public const string AirlineCompany1_USER_NAME = "ELAL12345";
        public const string AirlineCompany1_PASSWORD = "12345";

        //AirlineCompany2 Data
        public const string AirlineCompany2_AIRLINE_NAME = "Aeroflot";
        public const string AirlineCompany2_USER_NAME = "Aeroflot12345";
        public const string AirlineCompany2_PASSWORD = "123456";

        //Updated AirlineCompany Data
        public const string AirlineCompany_UpdatedName_AIRLINE_NAME = "El-AL TOURS";
        public const string AirlineCompany_NewPassword_PASSWORD = "ELAL1234567";

        //AdministratorFacade: Updated AirlineCompany Data
        public const string UpdateAirlineDetail_AIRLINE_NAME = "Arkia";
        public const string UpdateAirlineDetail_USER_NAME = "Arkia12345";
        public const string UpdateAirlineDetail_PASSWORD = "252525";

        //AirlineCompany Wrong Password
        public const string AirlineCompanyPasswordNotFound_PASSWORD = "8888";

        //Flight Data
        public static DateTime CreateNewFlight_DEPARTURE_TIME = DateTime.ParseExact("2019-07-08 12:00:00", "yyyy-MM-dd HH:mm:ss", null);
        public static DateTime CreateNewFlight_LANDING_TIME = DateTime.ParseExact("2019-07-08 18:00:00", "yyyy-MM-dd HH:mm:ss", null);
        public const int CreateNewFlight_REMANING_TICKETS = 100;
        public const int CreateNewFlight_TOTAL_TICKETS = 100;

        //Updated Flight1 Data
        public static DateTime UpdateFlight1Detail_DEPARTURE_TIME = DateTime.ParseExact("2019-07-08 11:00:00", "yyyy-MM-dd HH:mm:ss", null);
        public static DateTime UpdateFlight1Detail_LANDING_TIME = DateTime.ParseExact("2019-07-08 17:00:00", "yyyy-MM-dd HH:mm:ss", null);
        public const int UpdateFlight1Detail_REMANING_TICKETS = 5;
        public const int UpdateFlight1Detail_TOTAL_TICKETS = 100;

        //Updated Flight2 Data
        public static DateTime Flight2Detail_DEPARTURE_TIME = DateTime.ParseExact("2019-07-08 10:00:00", "yyyy-MM-dd HH:mm:ss", null);
        public static DateTime Flight2Detail_LANDING_TIME = DateTime.ParseExact("2019-07-08 17:00:00", "yyyy-MM-dd HH:mm:ss", null);
        public const int Flight2Detail_REMANING_TICKETS = 5;
        public const int Fligh2tDetail_TOTAL_TICKETS = 100;

        //Country1 Data
        public const string CreateNewCountry1_AddCountry_COUNTRY_NAME = "Israel";

        //Country2 Data
        public const string CreateNewCountry2_AddCountry_COUNTRY_NAME = "Russia";

       
    }
}
