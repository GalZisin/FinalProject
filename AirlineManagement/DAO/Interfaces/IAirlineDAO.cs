using AirlineManagement.POCO.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement
{
    public interface IAirlineDAO : IBasicDB<AirlineCompany>
    {
        AirlineCompany GetAirlineByUsername(string name);
        IList<AirlineCompany> GetAllAirlinesByCountry(long countryId);
        void RemoveAirlineCompanyById(long airlinecompanyId);
        void RemoveAirlineCompanyByCountryCode(long countryCode);
        AirlineCompany GetAirlineCompanyByAirlineName(string AirlineName);
        string CheckIfAirlineCompanyExist(AirlineCompany t);
        IList<AirlineCompany> GetAllAirlineCompaniesByScheduledTime(string typeName);
        string CheckIfAirlineCompanyExistById(AirlineCompany t);
        IList<AirlineCompanyView> GetAllAirlineCompanies();
        AirlineCompanyView GetGetAirlineCompanyById(long id);
        long AddAirlineCompany(AirlineCompanyView t);
        void UpdateAirlineCompany(AirlineCompanyView t);
    }
}
