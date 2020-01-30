using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement
{
    public class Customer: IPoco, IUser
    {
        public Customer()
        {
        }

        public Customer(long iD, string fIRST_NAME, string lAST_NAME, string uSER_NAME, string pASSWORD, string aDDRESS, string pHONE_NO, string cREDIT_CARD_NUMBER)
        {
            ID = iD;
            FIRST_NAME = fIRST_NAME;
            LAST_NAME = lAST_NAME;
            USER_NAME = uSER_NAME;
            PASSWORD = pASSWORD;
            ADDRESS = aDDRESS;
            PHONE_NO = pHONE_NO;
            CREDIT_CARD_NUMBER = cREDIT_CARD_NUMBER;
        }

        public long ID { get; set; }
        public string FIRST_NAME { get; set; }
        public string LAST_NAME { get; set; }
        public string USER_NAME { get; set; }
        public string PASSWORD { get; set; }
        public string ADDRESS { get; set; }
        public string PHONE_NO { get; set; }
        public string CREDIT_CARD_NUMBER { get; set; }
        public static bool operator ==(Customer thisCustomer, Customer otherCustomer)
        {
            if (ReferenceEquals(thisCustomer, null) && ReferenceEquals(otherCustomer, null))
                return true;
            if (ReferenceEquals(thisCustomer, null) || ReferenceEquals(otherCustomer, null))
                return false;
            return thisCustomer.ID == otherCustomer.ID;
        }
        public static bool operator !=(Customer thisCustomer, Customer otherCustomer)
        {
            if (thisCustomer == null && otherCustomer == null)
                return false;
            if (thisCustomer == null || otherCustomer == null)
                return true;
            return !(thisCustomer.ID == otherCustomer.ID);
        }

        public override bool Equals(object obj)
        {
            var customer = obj as Customer;
            if (ReferenceEquals(customer, null))
                return false;

            return this.ID == customer.ID;
        }

        public override int GetHashCode()
        {
            return this.ID.GetHashCode();
        }
    }
}
