namespace StallosDotnetPleno.Domain.Entities.Models
{
    public class Address : Entity
    {
        public string PostCode { get; set; }

        public string Street { get; set; }

        public string Number { get; set; }

        public string District { get; set; }

        public string City { get; set; }

        public string StateCode { get; set; }
        
        private Address(string postCode, string street, string number, string district, string city, string stateCode) 
        { 
            this.PostCode = postCode;
            this.Street = street;
            this.Number = number;
            this.District = district;
            this.City = city;
            this.StateCode = stateCode;
        }

        public static Address New(string postCode, string street, string number, string district, string city, string stateCode) 
            => new Address(postCode, street, number, district, city, stateCode);
    }
}
