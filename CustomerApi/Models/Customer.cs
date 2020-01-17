using System;

namespace CustomerApi.Models
{
    public class Customer
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MachineName
        {
            get
            {
                   return Environment.MachineName;
            }
        }
        public string FullName
        {
            get
            {
                return $"{FirstName} ${LastName}";
            }
        }

        public Customer(long id, string firstName, string lastName) {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
        }
    }
}