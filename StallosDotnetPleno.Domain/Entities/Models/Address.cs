namespace StallosDotnetPleno.Domain.Entities.Models
{
    public class Address : BaseEntity
    {
        public string PostCode { get; private set; }

        public string Street { get; private set; }

        public string Number { get; private set; }

        public string District { get; private set; }

        public string City { get; private set; }

        public string StateCode { get; private set; }
        
        private Address(string postCode, string street, string number, string district, string city, string stateCode) 
        { 
            this.PostCode = postCode;
            this.Street = street;
            this.Number = number;
            this.District = district;
            this.City = city;
            this.StateCode = stateCode;

            // Add entity validation
        }

        public static Address New(string postCode, string street, string number, string district, string city, string stateCode) 
            => new Address(postCode, street, number, district, city, stateCode);
    }
}
