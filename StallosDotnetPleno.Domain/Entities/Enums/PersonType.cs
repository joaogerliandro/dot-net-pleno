using System.ComponentModel;

namespace StallosDotnetPleno.Domain.Entities.Enums
{
    public enum PersonType
    {
        [Description("Pessoa Física")]
        PF = 0,
        [Description("Pessoa Jurídica")]
        PJ = 1
    }
}
