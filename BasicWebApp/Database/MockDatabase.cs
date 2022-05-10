using System.Collections.Generic;
using System.Linq;

namespace BasicWebApp.Database
{
    public class MockDatabase : IDatabase
    {
        private List<Person> _personList;
        public int? NextId { get; set; }

        public MockDatabase()
        {
            _personList = new List<Person>();
            NextId = 1;
        }

        public List<Person> GetPersonList()
        {
            return _personList;
        }

        public Person GetPersonInfo(int? personId)
        {
            return _personList.First(person => person.Id == personId);
        }

        public void AddPerson(Person person)
        {
            person.setId(NextId);
            NextId++;
            _personList.Add(person);
        }

        public void DeletePerson(int? personId)
        {
            int index = _personList.FindIndex(p => p.Id == personId);
            _personList.RemoveAt(index);
        }

        public void UpdatePerson(int? personId, Person person)
        {
            int index = _personList.FindIndex(p => p.Id == personId);
            person.setId(personId);
            _personList[index] = person;
        }
    }
}