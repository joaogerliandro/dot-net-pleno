namespace StallosDotnetPleno.Application.DTOS
{
    public class PersonDto
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string TipoPessoa { get; set; }
        public string Documento { get; set; }
        public List<AddressDto> Endereços { get; set; }
    }
}
