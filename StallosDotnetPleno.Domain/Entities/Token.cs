namespace StallosDotnetPleno.Domain.Entities
{
    public class Token
    {
        public string Key { get; set; }

        public DateTime? ExpirationDateTime { get; set; }
    }
}
