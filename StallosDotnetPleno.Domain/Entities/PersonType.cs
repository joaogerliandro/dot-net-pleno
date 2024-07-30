using StallosDotnetPleno.Domain.Enums;

namespace StallosDotnetPleno.Domain.Entities
{
    public class PersonType : BaseEntity
    {
        public PersonTypeEnum Type { get; set; }

        public PersonType() { }

        public PersonType(PersonTypeEnum type)
        {
            Type = type;
        }
    }
}