using System;
using System.Collections.Generic;
using BasicWebApp;
using BasicWebApp.Database;
using BasicWebApp.Exceptions;
using Xunit;

namespace BasicWebAppTests
{
    public class ServiceGreetingTests
    {
        [Fact]
        public void given_DatabaseContainsOneName_when_GetGroupGreeting_then_return_GreetingForPersonOne()
        {
            IDatabase mockDatabase = new MockDatabase();
            mockDatabase.AddPerson(new Person("Mark"));

            ServiceGreeting service =
                new ServiceGreeting(mockDatabase, "11:30:14 am", "Wednesday, 23 February 2022");

            String response = service.GetGroupGreeting();
            
            Assert.Equal("Hello Mark, the time on the server is 11:30:14 am Wednesday, 23 February 2022", response);
        }
        
        [Fact]
        public void given_DatabaseContainsTwoPeople_when_GetGroupGreeting_then_return_GreetingForTwoPeople()
        {
            IDatabase mockDatabase = new MockDatabase();
            mockDatabase.AddPerson(new Person("Mark"));
            mockDatabase.AddPerson(new Person("Bob"));

            ServiceGreeting service =
                new ServiceGreeting(mockDatabase, "11:30:14 am", "Wednesday, 23 February 2022");

            String response = service.GetGroupGreeting();
            
            Assert.Equal("Hello Mark and Bob, the time on the server is 11:30:14 am Wednesday, 23 February 2022", response);
        }
        
        [Fact]
        public void given_PersonIDEqualsTwo_when_GetIndividualGreeting_then_return_GreetingForPersonTwo()
        {
            IDatabase mockDatabase = new MockDatabase();
            mockDatabase.AddPerson(new Person("Mark"));
            mockDatabase.AddPerson(new Person("Bob"));

            ServiceGreeting service =
                new ServiceGreeting(mockDatabase, "11:30:14 am", "Wednesday, 23 February 2022");

            String response = service.GetIndividualGreeting(2);
            
            Assert.Equal("Hello Bob, the time on the server is 11:30:14 am Wednesday, 23 February 2022", response);
        }
        
        [Fact]
        public void given__PersonIDDoesNotExist_when_GetIndividualGreeting_then_ThrowIdDoesNotExistException()
        {
            IDatabase mockDatabase = new MockDatabase();
            mockDatabase.AddPerson(new Person("Mark"));
            mockDatabase.AddPerson(new Person("Bob"));

            ServiceGreeting service =
                new ServiceGreeting(mockDatabase, "11:30:14 am", "Wednesday, 23 February 2022");

            Assert.Throws<IdDoesNotExistException>(() => service.GetIndividualGreeting(3));
        }
    }
}