using System.Collections.Generic;
using BasicWebApp;
using BasicWebApp.Database;
using BasicWebApp.DTO;
using BasicWebApp.Exceptions;
using BasicWebApp.Services;
using Xunit;

namespace BasicWebAppTests
{
    public class ServicePersonTests
    {
        [Fact]
        public void given_InitialDataContainsValidName_when_GetPersonList_then_return_ListContainingSameName()
        {
            IDatabase mockDatabase = new MockDatabase();
            mockDatabase.AddPerson(new Person("Mark"));

            ServicePerson servicePerson = new ServicePerson(mockDatabase);
        
            List<Person> answer = servicePerson.GetPersonList();
        
            Assert.Contains(answer, person => person.Name == "Mark");
        }
        
        [Fact]
        public void given_NewPersonHasValidName_when_AddPerson_then_AddPersonWithSameNameToDatabase()
        {
            IDatabase mockDatabase = new MockDatabase();
            mockDatabase.AddPerson(new Person("Mark"));

            ServicePerson servicePerson = new ServicePerson(mockDatabase);
            
            PersonDTO addPersonDto = new PersonDTO() {Name = "Bob"};
        
            servicePerson.AddPerson(addPersonDto);
        
            List<Person> personList = servicePerson.GetPersonList();
        
            Assert.Contains(personList, person => person.Name == "Mark");
            Assert.Contains(personList, person => person.Name == "Bob");
        }
        
        [Fact]
        public void given_ValidId_when_DeletePerson_then_RemovePersonWithSameId()
        {
            IDatabase mockDatabase = new MockDatabase();
            mockDatabase.AddPerson(new Person("Mark"));
            mockDatabase.AddPerson(new Person("Andy"));

            ServicePerson servicePerson = new ServicePerson(mockDatabase);

            servicePerson.DeletePerson(2);
        
            List<Person> personList = servicePerson.GetPersonList();
            
            Assert.Contains(personList, person => person.Name == "Mark");
            Assert.DoesNotContain(personList, person => person.Name == "Andy");
        }
        
        [Fact]
        public void given_InvalidId_when_DeletePerson_then_ThrowIdDoesNotExistException()
        {
            IDatabase mockDatabase = new MockDatabase();
            mockDatabase.AddPerson(new Person("Mark"));
            mockDatabase.AddPerson(new Person("Andy"));

            ServicePerson servicePerson = new ServicePerson(mockDatabase);

            Assert.Throws<IdDoesNotExistException>(() => servicePerson.DeletePerson(3));
        }
        
        [Fact]
        public void given_InitialDataContainsValidName_PersonToAddHasSameName_when_AddPerson_then_DoNotAddPersonToPersonList()
        {
            IDatabase mockDatabase = new MockDatabase();
            mockDatabase.AddPerson(new Person("Mark"));
            mockDatabase.AddPerson(new Person("Mary"));

            ServicePerson servicePerson = new ServicePerson(mockDatabase);
            PersonDTO addPersonDto = new PersonDTO() {Name = "Mary"};
        
            servicePerson.AddPerson(addPersonDto);
        
            List<Person> personList = servicePerson.GetPersonList();
            
            Assert.Equal(2, personList.Count);
        }

        [Fact]
        public void given_NewNameEqualsValidName_and_ValidId_when_UpdatePerson_then_PersonWithSameIdHasNewName()
        {
            IDatabase mockDatabase = new MockDatabase();
            mockDatabase.AddPerson(new Person("Mark"));
            mockDatabase.AddPerson(new Person("Andy"));

            ServicePerson servicePerson = new ServicePerson(mockDatabase);
            PersonDTO personDto = new PersonDTO(){Name = "Bob", Id = 2};
            
            servicePerson.UpdatePerson(personDto);
        
            List<Person> personList = servicePerson.GetPersonList();
        
            Assert.Equal("Mark", personList.Find(p => p.Id == 1).Name);
            Assert.Equal("Bob", personList.Find(p => p.Id == 2).Name);
            Assert.DoesNotContain(personList, person => person.Name == "Andy");
        }
        
        [Fact]
        public void given_PersonTwoHasValidName_and_ValidId_when_GetPersonInfo_then_return_PersonWithSameName()
        {
            IDatabase mockDatabase = new MockDatabase();
            mockDatabase.AddPerson(new Person("Mark"));
            mockDatabase.AddPerson(new Person("Bob"));

            ServicePerson servicePerson = new ServicePerson(mockDatabase);
        
            Person answer = servicePerson.GetPersonInfo(2);
            
            Assert.Equal("Bob", answer.Name);
        }
        
        [Fact]
        public void given_InvalidId_when_UpdatePerson_then_throwIdDoesNotExistException()
        {
            IDatabase mockDatabase = new MockDatabase();
            mockDatabase.AddPerson(new Person("Mark"));

            ServicePerson servicePerson = new ServicePerson(mockDatabase);

            PersonDTO personDto = new PersonDTO(){Name = "Bob", Id = 2};

            Assert.Throws<IdDoesNotExistException>(() => servicePerson.UpdatePerson(personDto));
        }
        
        [Fact]
        public void given_InvalidId_when_GetPersonInfo_throwIdDoesNotExistException()
        {
            IDatabase mockDatabase = new MockDatabase();
            mockDatabase.AddPerson(new Person("Mark"));

            ServicePerson servicePerson = new ServicePerson(mockDatabase);
        
            Assert.Throws<IdDoesNotExistException>(() => servicePerson.GetPersonInfo(2));
        }
    }
}