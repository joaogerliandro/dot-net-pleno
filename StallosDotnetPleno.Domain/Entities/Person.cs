using StallosDotnetPleno.Domain.Enums;

namespace StallosDotnetPleno.Domain.Entities
{
    public class Person : BaseEntity
    {
        public string Name { get; private set; }

        public PersonType Type { get; private set; }

        public string Document { get; private set; }

        public ICollection<Address> Addresses { get; private set; }

        private Person() { }

        public Person(string name, PersonType type, string document, ICollection<Address> addresses)
        {
            Name = name;
            Type = type;
            Document = document;
            Addresses = addresses;
        }
    }
}
