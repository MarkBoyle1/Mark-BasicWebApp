using System;
using System.Collections.Generic;
using BasicWebApp.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace BasicWebApp.DTO
{
    public class DTOGenerator : IDTOGenerator
    {
        public PersonDTO GeneratePersonDTO(string requestBody, int? id = null)
        {
            CheckRequestBodyIsValid(requestBody);
            PersonDTO personDto = JsonSerializer.Deserialize<PersonDTO>(requestBody);
            personDto.Id = id;

            return personDto;
        }

        private void CheckRequestBodyIsValid(string requestBody)
        {
            if (String.IsNullOrWhiteSpace(requestBody))
            {
                throw new EmptyBodyException();
            }
            try
            {
                var jsonObject = JObject.Parse(requestBody);
                var dictionaryRequestBody = jsonObject.ToObject<Dictionary<string, JToken?>>();
                if (!dictionaryRequestBody.ContainsKey("Name"))
                {
                    throw new InvalidBodyException();
                }
            }
            catch (JsonReaderException)
            {
                throw new InvalidBodyException();
            }
        }
    }
}