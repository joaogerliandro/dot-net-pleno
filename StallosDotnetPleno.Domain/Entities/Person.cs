using StallosDotnetPleno.Domain.Enums;
using StallosDotnetPleno.Domain.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace StallosDotnetPleno.Domain.Entities
{
    public class Person : BaseEntity
    {
        public string Name { get; private set; }

        [ForeignKey("PersonTypeId")]
        public PersonType Type { get; private set; }

        public long PersonTypeId { get; private set; }

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
