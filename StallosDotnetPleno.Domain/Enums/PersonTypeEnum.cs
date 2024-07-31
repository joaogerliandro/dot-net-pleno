using System.ComponentModel;

namespace StallosDotnetPleno.Domain.Enums
{
    public enum PersonTypeEnum
    {
        [Description("Pessoa Física")]
        PF = 0,
        [Description("Pessoa Jurídica")]
        PJ = 1
    }
}
