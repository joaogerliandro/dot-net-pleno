using StallosDotnetPleno.Domain.Entities.Enums;

namespace StallosDotnetPleno.Domain.Entities.Models
{
    public class Person : BaseEntity
    {
        public string Name { get; private set; }

        public PersonType Type { get; private set; }

        public string Document {  get; private set; }

        private Person (string name, PersonType type, string document)
        {
            this.Name = name;
            this.Type = type;
            this.Document = document;

            // Add entity validation
        }

        public static Person New(string name, PersonType type, string document) => new Person(name, type, document);
    }
}
