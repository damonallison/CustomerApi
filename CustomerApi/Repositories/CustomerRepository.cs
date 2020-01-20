using System.Collections.Generic;
using System.Linq;

using CustomerApi.Models;

using MongoDB.Driver;

namespace CustomerApi.Repositories
{
    public class CustomerRepository
    {
        private readonly IMongoCollection<Customer> _customers;

        public CustomerRepository(ICustomerDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _customers = database.GetCollection<Customer>(settings.CollectionName);
        }

        public IList<Customer> GetCustomers()
        {
            return _customers.Find(customer => true).ToList();
        }

        public Customer Get(string id)
        {
            return _customers.Find<Customer>(customer => customer.Id == id).FirstOrDefault();
        }

        public Customer Create(Customer customer)
        {
            _customers.InsertOne(customer);
            return customer;
        }

        public void Update(string id, Customer customerIn)
        {
            _customers.ReplaceOne(customer => customer.Id == id, customerIn);
        }

        public void Remove(string id)
        {
            _customers.DeleteOne(customer => customer.Id == id);
        }

    }

}