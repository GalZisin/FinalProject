using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineManagement
{
    public interface ICustomerDAO : IBasicDB<Customer>
    {
        Customer GetCustomerByUsername(string name);
        Customer GetCustomerByName(string customerName);
        string CheckIfCustomerExist(Customer t);
        string CheckIfCustomerExistById(Customer t);
    }
}
