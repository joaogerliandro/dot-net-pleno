using StallosDotnetPleno.Domain.Interfaces;

namespace StallosDotnetPleno.Domain.Entities
{
    public class Address : BaseEntity
    {
        public string PostCode { get; private set; }

        public string Street { get; private set; }

        public string Number { get; private set; }

        public string District { get; private set; }

        public string City { get; private set; }

        public string StateCode { get; private set; }

        private Address(string postCode, string street, string number, string district, string city, string stateCode, IValidator<Address> validator)
        {
            PostCode = postCode;
            Street = street;
            Number = number;
            District = district;
            City = city;
            StateCode = stateCode;

            SetValidator(validator);
            Validate();
        }
    }
}
