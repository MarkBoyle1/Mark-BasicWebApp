using System.Collections.Generic;

namespace BasicWebApp.Database
{
    public interface IDatabase
    {
        public List<Person> GetPersonList();

        public void AddPerson(Person person);

        public void DeletePerson(int? personId);

        public void UpdatePerson(int? personId, Person person);

        public Person GetPersonInfo(int? personId);
    }
}