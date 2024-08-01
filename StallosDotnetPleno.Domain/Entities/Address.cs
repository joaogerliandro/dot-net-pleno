namespace StallosDotnetPleno.Domain.Entities
{
    public class Address : BaseEntity
    {
        public string ZipCode { get; private set; }

        public string Street { get; private set; }

        public string Number { get; private set; }

        public string District { get; private set; }

        public string City { get; private set; }

        public string StateCode { get; private set; }

        public ICollection<Person> Persons { get; set; }

        private Address() { }

        public Address(string zipCode, string street, string number, string district, string city, string stateCode, ICollection<Person> persons)
        {
            ZipCode = zipCode;
            Street = street;
            Number = number;
            District = district;
            City = city;
            StateCode = stateCode;
            Persons = persons;
        }

        public void Update(Address updatedAddress)
        {
            ZipCode = updatedAddress.ZipCode;
            Street = updatedAddress.Street;
            Number = updatedAddress.Number;
            District = updatedAddress.District;
            City = updatedAddress.City;
            StateCode = updatedAddress.StateCode;
        }
    }
}
