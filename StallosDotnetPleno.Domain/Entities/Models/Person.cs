using StallosDotnetPleno.Domain.Entities.Enums;

namespace StallosDotnetPleno.Domain.Entities.Models
{
    public class Person : Entity
    {
        public string Name { get; set; }

        public PersonType Type { get; set; }

        public string Document {  get; set; }

        private Person (string name, PersonType type, string document)
        {
            this.Name = name;
            this.Type = type;
            this.Document = document;
        }

        public static Person New(string name, PersonType type, string document) => new Person(name, type, document);
    }
}
