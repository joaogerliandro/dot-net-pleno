using StallosDotnetPleno.Domain.Enums;

namespace StallosDotnetPleno.Domain.Entities
{
    public class Person : BaseEntity
    {
        public string Name { get; private set; }

        public PersonType Type { get; private set; }

        public string Document { get; private set; }

        private Person(string name, PersonType type, string document)
        {
            Name = name;
            Type = type;
            Document = document;

            // Add entity validation
        }

        public static Person New(string name, PersonType type, string document) => new Person(name, type, document);
    }
}
