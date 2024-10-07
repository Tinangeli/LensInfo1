using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace LensInfo1
{
    class Customer
    {
        public int IDCustomer { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }


    }

    class CustomerData
    {
        public static LensInfo1.CustomerData _instance;

        public static CustomerData Instance => _instance ?? (_instance = new CustomerData());

        public ObservableCollection<Customer> Customers { get; set; }

        public CustomerData() { 
            Customers = new ObservableCollection<Customer>();
        
        }

    }
}
