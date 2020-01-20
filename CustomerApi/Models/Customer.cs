using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CustomerApi.Models
{
    public class Customer
    {
        /// BsonId designates this property is the document's primary key
        ///
        /// BsonRepresentation Allows passing the parameter as `string` rather
        /// than an ObjectId structure.
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("firstName")]
        public string FirstName { get; set; }

        [BsonElement("lastName")]
        public string LastName { get; set; }

        [BsonIgnore]
        public string MachineName
        {
            get
            {
                   return Environment.MachineName;
            }
        }

        [BsonIgnore]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }

        /// A parameterless constructor is required for JSON deserialization
        public Customer() {}

        public Customer(string id, string firstName, string lastName) {
            this.Id = id;
            this.FirstName = firstName;
            this.LastName = lastName;
        }
    }
}