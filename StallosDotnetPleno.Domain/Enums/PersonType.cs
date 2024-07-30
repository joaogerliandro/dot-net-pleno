using System.ComponentModel;

namespace StallosDotnetPleno.Domain.Enums
{
    public enum PersonType
    {
        [Description("Pessoa Física")]
        PF = 0,
        [Description("Pessoa Jurídica")]
        PJ = 1
    }
}
