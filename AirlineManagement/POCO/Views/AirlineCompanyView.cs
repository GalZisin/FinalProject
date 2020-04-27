using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement.POCO.Views
{
    public class AirlineCompanyView : AirlineCompany
    {
        public AirlineCompanyView()
        {
           
        }

        public AirlineCompanyView(long iD, string aIRLINE_NAME, string uSER_NAME, string pASSWORD, long cOUNTRY_CODE, string cOUNTRY_NAME) : base(iD, aIRLINE_NAME, uSER_NAME, pASSWORD, cOUNTRY_CODE)
        {
            COUNTRY_NAME = cOUNTRY_NAME;
        }

        public string COUNTRY_NAME { get; set; }
    }
}
