using System.Collections.Generic;
using CustomerApi.Models;

namespace Repositories
{
    public class CustomerRepository
    {
        public List<Customer> GetCustomers()
        {
            return new List<Customer> {
                new Customer(1, "Damon", "Allison"),
                new Customer(2, "Kari", "Allison"),
            };
        }
    }
}