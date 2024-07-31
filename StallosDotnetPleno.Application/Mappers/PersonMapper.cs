using StallosDotnetPleno.Application.DTOS;
using StallosDotnetPleno.Domain.Entities;
using System.Net;

namespace StallosDotnetPleno.Application.Mappers
{
    public static class PersonMapper
    {
        public static PersonDto ToDto(this Person person)
        {
            return new PersonDto
            {
                Id = person.Id,
                Nome = person.Name,
                TipoPessoa = person.RealType.Type.ToString(),
                Documento = person.Document,
                Endereços = person.Addresses.Select(address => new AddressDto
                {
                    Cep = address.ZipCode,
                    Rua = address.Street,
                    Numero = address.Number,
                    Bairro = address.District,
                    Cidade = address.City,
                    UF = address.StateCode
                }).ToList()
            };
        }

        public static IEnumerable<PersonDto> ToDtoList(this IEnumerable<Person> persons)
        {
            return persons.Select(person => person.ToDto()).ToList();
        }
    }
}