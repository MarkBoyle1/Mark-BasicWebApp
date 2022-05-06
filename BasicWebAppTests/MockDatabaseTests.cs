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
        public void given_InitialDataContainsMark_when_GetPersonList_then_return_ListContainingMark()
        {
            List<Person> initialData = new List<Person>() {new Person("Mark")};
            MockDatabase database = new MockDatabase(initialData);

            List<Person> personList = database.GetPersonList();

            Assert.Contains(personList, person => person.Name == "Mark");
        }
        
        [Fact]
        public void given_PersonIdEqualsTwo_when_GetPersonInfo_then_return_InfoForPersonTwo()
        {
            List<Person> initialData = new List<Person>()
            {
                new Person("Mark"),
                new Person("Andy")
            };
            MockDatabase database = new MockDatabase(initialData);

            Person person = database.GetPersonInfo(2);

            Assert.Contains("Andy", person.Name);
        }
        
        [Fact]
        public void given_PersonNameEqualsBob_when_AddPerson_then_return_ListContainingBob()
        {
            List<Person> initialData = new List<Person>() {new Person("Mark")};
            MockDatabase database = new MockDatabase(initialData);
            Person person = new Person("Bob");
            database.AddPerson(person);

            List<Person> personList = database.GetPersonList();

            Assert.Contains(personList, person => person.Name == "Bob");
        }
        
        [Fact]
        public void given_ListContainsOne_when_DeletePerson_then_return_EmptyList()
        {
            List<Person> initialData = new List<Person>() {new Person("Mark")};
            MockDatabase database = new MockDatabase(initialData);

            database.DeletePerson(1);

            List<Person> personList = database.GetPersonList();

            Assert.Empty(personList);
        }
        
        [Fact]
        public void setIdsOnInitialSetUp()
        {
            List<Person> initialData = new List<Person>()
            {
                new Person("Mark"),
                new Person("Andy")
            };
            MockDatabase database = new MockDatabase(initialData);

            List<Person> personList = database.GetPersonList();
            
            Assert.Equal(1, personList.Find(p => p.Name == "Mark").Id);
            Assert.Equal(2, personList.Find(p => p.Name == "Andy").Id);
        }
        
        [Fact]
        public void given_PersonTwoNameEqualsAndy_and_NewDataNameEqualsBob_when_UpdatePerson_then_PersonTwoNameEqualsBob()
        {
            List<Person> initialData = new List<Person>()
            {
                new Person("Mark"),
                new Person("Andy")
            };
            MockDatabase database = new MockDatabase(initialData);
            Person newData = new Person("Bob");
            
            database.UpdatePerson(2, newData);

            List<Person> personList = database.GetPersonList();

            Assert.Equal("Bob", personList.Find(p => p.Id == 2).Name);
            Assert.Equal("Mark", personList.Find(p => p.Id == 1).Name);
        }
    }
}