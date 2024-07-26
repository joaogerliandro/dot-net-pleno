using StallosDotnetPleno.Domain.Enums;
using StallosDotnetPleno.Domain.Interfaces;
using System;

namespace StallosDotnetPleno.Domain.Entities
{
    public class Person : BaseEntity
    {
        public string Name { get; private set; }

        public PersonType Type { get; private set; }

        public string Document { get; private set; }

        public ICollection<Address> Addresses { get; set; }

        public Person(string name, PersonType type, string document, IValidator<Person> validator)
        {
            Name = name;
            Type = type;
            Document = document;
            Addresses = new HashSet<Address>();

            SetValidator(validator);
            Validate();
        }
    }
}
