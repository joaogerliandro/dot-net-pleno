using StallosDotnetPleno.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace StallosDotnetPleno.Domain.Entities
{
    public class Person : BaseEntity
    {
        public string Name { get; private set; }

        [JsonIgnore]
        public PersonType RealType { get; set; }

        [NotMapped]
        public string Type { get; private set; }

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

        public void PrepareToDatabase(PersonType realType)
        {
            RealType = realType;
            Document = Regex.Replace(Document, @"[^\d]", "");
        }
    }
}
