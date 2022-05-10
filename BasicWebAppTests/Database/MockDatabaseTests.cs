using System;
using System.Collections.Generic;
using BasicWebApp;
using BasicWebApp.Database;
using Xunit;

namespace BasicWebAppTests
{
    public class MockDatabaseTests
    {
        [Fact]
        public void given_InitialDataContainsValidName_when_GetPersonList_then_return_ListContainingSameName()
        {
            MockDatabase database = new MockDatabase();
            database.AddPerson(new Person("Mark"));

            List<Person> personList = database.GetPersonList();

            Assert.Contains(personList, person => person.Name == "Mark");
        }
        
        [Fact]
        public void given_PersonIdEqualsValidId_when_GetPersonInfo_then_return_InfoForPersonWithSameId()
        {
            MockDatabase database = new MockDatabase();
            database.AddPerson(new Person("Mark"));
            database.AddPerson(new Person("Andy"));
            
            Person person = database.GetPersonInfo(2);

            Assert.Contains("Andy", person.Name);
        }
        
        [Fact]
        public void given_PersonNameEqualsValidName_when_AddPerson_then_return_ListContainingSameName()
        {
            MockDatabase database = new MockDatabase();
            database.AddPerson(new Person("Mark"));

            Person person = new Person("Bob");
            database.AddPerson(person);

            List<Person> personList = database.GetPersonList();

            Assert.Contains(personList, person => person.Name == "Bob");
        }
        
        [Fact]
        public void given_ValidId_when_DeletePerson_then_return_ListWithOneLessPerson()
        {
            MockDatabase database = new MockDatabase();
            database.AddPerson(new Person("Mark"));

            database.DeletePerson(1);

            List<Person> personList = database.GetPersonList();

            Assert.Empty(personList);
        }
        
        [Fact]
        public void given_setIdsOnInitialSetUp()
        {
            MockDatabase database = new MockDatabase();
            database.AddPerson(new Person("Mark"));
            database.AddPerson(new Person("Andy"));
            
            List<Person> personList = database.GetPersonList();
            
            Assert.Equal(1, personList.Find(p => p.Name == "Mark").Id);
            Assert.Equal(2, personList.Find(p => p.Name == "Andy").Id);
        }
        
        [Fact]
        public void given_ValidId_and_NewDataNameEqualsValidName_when_UpdatePerson_then_PersonWithSameIdHasUpdatedName()
        {
            MockDatabase database = new MockDatabase();
            database.AddPerson(new Person("Mark"));
            database.AddPerson(new Person("Andy"));

            Person newData = new Person("Bob");
            
            database.UpdatePerson(2, newData);

            List<Person> personList = database.GetPersonList();

            Assert.Equal("Bob", personList.Find(p => p.Id == 2).Name);
            Assert.Equal("Mark", personList.Find(p => p.Id == 1).Name);
        }
    }
}