using System;
using System.Collections.Generic;
using BasicWebApp.DTO;
using BasicWebApp.Exceptions;
using BasicWebApp.Services;
using Newtonsoft.Json;

namespace BasicWebApp
{
    public class ControllerPerson : IController
    {
        IDTOGenerator _dtoGenerator;
        private IServicePerson _servicePerson;
        private int _statusCode;

        public ControllerPerson(IServicePerson service, IDTOGenerator dtoGenerator)
        {
            _dtoGenerator = dtoGenerator;
            _servicePerson = service;
        }
        public Response ProcessRequest(Request request)
        {
            try
            {
                switch (request.HttpVerb)
                {
                    case Constants.POST:
                        PersonDTO addPersonDto = _dtoGenerator.GeneratePersonDTO(request.Body);
                        int? newPersonId = _servicePerson.AddPerson(addPersonDto);
                        
                        return new Response(newPersonId.ToString(), Constants.StatusCodeCreated);
                    
                    case Constants.DELETE:
                        if (request.PersonId == null)
                        {
                            throw new MissingIdException();
                        }
                        _servicePerson.DeletePerson(request.PersonId);
                        return new Response(String.Empty, Constants.StatusCodeOk);
                    
                    case Constants.PUT:
                        if (request.PersonId == null)
                        {
                            throw new MissingIdException();
                        }
                        PersonDTO updatedPersonDto =
                            _dtoGenerator.GeneratePersonDTO(request.Body, request.PersonId);
                        _servicePerson.UpdatePerson(updatedPersonDto);
                        
                        return new Response(String.Empty, Constants.StatusCodeOk);

                    case Constants.GET:
                        List<Person> responseData = new List<Person>();
                        
                        if (request.PersonId == null)
                        {
                            responseData = _servicePerson.GetPersonList();
                        }
                        else
                        {
                            responseData.Add(_servicePerson.GetPersonInfo(request.PersonId));
                        }
                        
                        string responseBody = JsonConvert.SerializeObject(responseData);
                        
                        return new Response(responseBody, Constants.StatusCodeOk);
                    
                    default:
                        return new Response(String.Empty, Constants.StatusCodeMethodNotAllowed);
                }
            }
            catch (EmptyBodyException)
            {
                return new Response(Messages.EmptyBodyMessage, Constants.StatusCodeBadRequest);
            }
            catch (InvalidBodyException)
            {
                return new Response(Messages.InvalidBodyMessage, Constants.StatusCodeBadRequest);
            }
            catch (IdDoesNotExistException)
            {
                return new Response(Messages.InvalidIdMessage, Constants.StatusCodeNotFound);
            }
            catch (MissingIdException)
            {
                return new Response(Messages.MissingIdMessage, Constants.StatusCodeBadRequest);
            }
        }
    }
}