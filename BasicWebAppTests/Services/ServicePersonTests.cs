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
        public void given_InitialDataContainsPersonNamedMark_when_GetPersonList_then_return_ListContainingMark()
        {
            IDatabase mockDatabase = new MockDatabase();
            mockDatabase.AddPerson(new Person("Mark"));

            ServicePerson servicePerson = new ServicePerson(mockDatabase);
        
            List<Person> answer = servicePerson.GetPersonList();
        
            Assert.Contains(answer, person => person.Name == "Mark");
        }
        
        [Fact]
        public void given_NewPersonIsNamedBob_when_AddPerson_then_AddBobToDatabase()
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
        public void given_IdEqualsTwo_when_DeletePerson_then_RemovePersonWithIdOfTwoFromDatabase()
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
        public void given_IdEqualsThree_IdDoesNotExist_when_DeletePerson_then_ThrowIdDoesNotExistException()
        {
            IDatabase mockDatabase = new MockDatabase();
            mockDatabase.AddPerson(new Person("Mark"));
            mockDatabase.AddPerson(new Person("Andy"));

            ServicePerson servicePerson = new ServicePerson(mockDatabase);

            Assert.Throws<IdDoesNotExistException>(() => servicePerson.DeletePerson(3));
        }
        
        [Fact]
        public void given_InitialDataContainsPersonNamedMary_PersonToAddIsNamedMary_when_AddPerson_then_DoNotAddMaryToPersonList()
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
        public void given_NewNameEqualsAndy_and_PersonIdEqualsOne_when_UpdatePerson_then_PersonOneNameEqualsAndy()
        {
            IDatabase mockDatabase = new MockDatabase();
            mockDatabase.AddPerson(new Person("Mark"));

            ServicePerson servicePerson = new ServicePerson(mockDatabase);
            PersonDTO personDto = new PersonDTO(){Name = "Andy", Id = 1};
            
            servicePerson.UpdatePerson(personDto);
            
            List<Person> personList = servicePerson.GetPersonList();
        
            Assert.Contains(personList, person => person.Name == "Andy");
            Assert.DoesNotContain(personList, person => person.Name == "Mark");
        }
        
        [Fact]
        public void given_NewNameEqualsAndy_and_PersonIdEqualsTwo_when_UpdatePerson_then_PersonTwoNameEqualsBob()
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
        public void given_PersonTwoIsNamedBob_and_PersonIdEqualsTwo_when_GetPersonInfo_then_return_PersonNameEqualsBob()
        {
            IDatabase mockDatabase = new MockDatabase();
            mockDatabase.AddPerson(new Person("Mark"));
            mockDatabase.AddPerson(new Person("Bob"));

            ServicePerson servicePerson = new ServicePerson(mockDatabase);
        
            Person answer = servicePerson.GetPersonInfo(2);
            
            Assert.Equal("Bob", answer.Name);
        }
        
        [Fact]
        public void given_IdTwoDoesNotExist_when_UpdatePerson_then_throwIdDoesNotExistException()
        {
            IDatabase mockDatabase = new MockDatabase();
            mockDatabase.AddPerson(new Person("Mark"));

            ServicePerson servicePerson = new ServicePerson(mockDatabase);

            PersonDTO personDto = new PersonDTO(){Name = "Bob", Id = 2};

            Assert.Throws<IdDoesNotExistException>(() => servicePerson.UpdatePerson(personDto));
        }
        
        [Fact]
        public void given_IdTwoDoesNotExist_when_GetPersonInfo_throwIdDoesNotExistException()
        {
            IDatabase mockDatabase = new MockDatabase();
            mockDatabase.AddPerson(new Person("Mark"));

            ServicePerson servicePerson = new ServicePerson(mockDatabase);
        
            Assert.Throws<IdDoesNotExistException>(() => servicePerson.GetPersonInfo(2));
        }
    }
}