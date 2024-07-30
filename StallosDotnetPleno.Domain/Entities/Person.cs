﻿using StallosDotnetPleno.Domain.Enums;
using StallosDotnetPleno.Domain.Validators;

namespace StallosDotnetPleno.Domain.Entities
{
    public class Person : BaseEntity
    {
        public string Name { get; private set; }

        private PersonType _type { get; set; }

        public string Type {  get; private set; }

        public string Document { get; private set; }

        public ICollection<Address> Addresses { get; private set; }

        private Person() { }

        public Person(string name, string type, string document, ICollection<Address> addresses)
        {
            Name = name;
            Type = type;
            Document = document;
            Addresses = addresses;
        }
    }
}
