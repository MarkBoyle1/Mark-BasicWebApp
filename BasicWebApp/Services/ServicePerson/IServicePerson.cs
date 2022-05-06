using System.Collections.Generic;
using BasicWebApp.DTO;

namespace BasicWebApp.Services
{
    public interface IServicePerson
    {
        public List<Person> GetPersonList();
        public Person GetPersonInfo(int? personId);
        public int? AddPerson(PersonDTO addPersonDto);
        public void DeletePerson(int? id);
        public void UpdatePerson(PersonDTO personDto);
    }
}