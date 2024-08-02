namespace StallosDotnetPleno.Domain.Entities
{
    public class PublicList : BaseEntity
    {
        public string ListName {  get; private set; }

        public Person Person { get; private set; }

        private PublicList() { }

        public PublicList(string listName, Person person) 
        { 
            ListName = listName;
            Person = person;
        }
    }
}
