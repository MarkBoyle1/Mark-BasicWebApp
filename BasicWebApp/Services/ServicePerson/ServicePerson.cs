using System.Collections.Generic;
using System.Linq;
using BasicWebApp.Database;
using BasicWebApp.DTO;
using BasicWebApp.Exceptions;

namespace BasicWebApp.Services
{
    public class ServicePerson : IServicePerson
    {
        private readonly IDatabase _database;

        public ServicePerson(IDatabase database)
        {
            _database = database;
        }

        public List<Person> GetPersonList()
        {
            return _database.GetPersonList();
        }

        public Person GetPersonInfo(int? personId)
        {
            List<Person> personList = _database.GetPersonList();

            if (personList.Exists(p => p.Id == personId))
            {
                return _database.GetPersonInfo(personId);
            }

            throw new IdDoesNotExistException();
        }

        public int? AddPerson(PersonDTO addPersonDto)
        {
            List<Person> personList = _database.GetPersonList();
            Person person = new Person(addPersonDto.Name);

            if (!personList.Exists(p => p.Name == person.Name))
            {
                _database.AddPerson(person);
            }

            return person.Id;
        }

        public void DeletePerson(int? id)
        {
            List<Person> personList = _database.GetPersonList();

            if (personList.Exists(p => p.Id == id))
            {
                _database.DeletePerson(id);
            }
            else
            {
                throw new IdDoesNotExistException();
            }
        }

        public void UpdatePerson(PersonDTO personDto)
        {
            List<Person> personList = _database.GetPersonList();
            Person person = new Person(personDto.Name);

            if (personList.Exists(p => p.Id == personDto.Id))
            {
                _database.UpdatePerson(personDto.Id, person);
            }
            else
            {
                throw new IdDoesNotExistException();
            }
        }
    }
}
