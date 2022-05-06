using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BasicWebApp.Database;
using BasicWebApp.DTO;
using BasicWebApp.Services;

namespace BasicWebApp
{
    public class ServiceGreeting : IServiceGreeting
    {
        private string _time;
        private string _date;
        private ServicePerson _servicePerson;

        public ServiceGreeting(IDatabase database, string time = "", string date = "")
        {
            _time = time;
            _date = date;
            _servicePerson = new ServicePerson(database);
        }

        public string GetGroupGreeting()
        {
            List<Person> personList = _servicePerson.GetPersonList();
            return MakeGreeting(personList);
        }

        public string GetIndividualGreeting(int? personId)
        {
            List<Person> personList = new List<Person>() {_servicePerson.GetPersonInfo(personId)};
            return MakeGreeting(personList);
        }

        private string MakeGreeting(List<Person> personList)
        {
            if (_time == "" && _date == "")
            {
                _time = DateTime.Now.ToLongTimeString();
                _date = DateTime.Now.ToLongDateString();
            }

            StringBuilder names = new StringBuilder();

            foreach (var p in personList)
            {
                names.Append(FormatName(p.Name, personList));
            }
            
            return $"Hello {names}, the time on the server is {_time} {_date}";
        }
        
        private string FormatName(string name, List<Person> personList)
        {
            if (name == personList.Last().Name)
            {
                return name;
            }

            if (name == personList[personList.Count - 2].Name)
            {
                return name + " and ";
            }

            return name + ", ";
        }
    }
}