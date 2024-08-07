﻿using System.ComponentModel.DataAnnotations.Schema;
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

        [JsonIgnore]
        public ICollection<PublicList> PublicLists { get; private set; }

        private Person() { }

        public Person(string name, string type, string document, ICollection<Address> addresses)
        {
            Name = name;
            Type = type;
            Document = document;
            Addresses = addresses ?? new List<Address>();
        }

        public void PrepareToDatabase(PersonType realType)
        {
            RealType = realType;
            Document = Regex.Replace(Document, @"[^\d]", "");
        }

        public void UpdateEntity(Person changedPerson)
        {
            Name = changedPerson.Name;
            Type = changedPerson.Type;
        }

        public void UpdateDocument(string document)
        {
            Document = Document;
        }

        public void UpdatePublicLists(ICollection<PublicList> publicLists) 
        {
            PublicLists = publicLists;
        }
    }
}
