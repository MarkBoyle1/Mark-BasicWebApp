using System;
using System.Collections.Generic;
using BasicWebApp.Database;
using BasicWebApp.Exceptions;

namespace BasicWebApp.Controllers
{
    public class ControllerGreeting : IController
    {
        private readonly IServiceGreeting _service;
         private int _statusCode;

         public ControllerGreeting(IServiceGreeting service)
         {
             _service = service;
         }
         public Response ProcessRequest(Request request)
         {
             string responseBody;
             
             try
             {
                 switch (request.HttpVerb)
                 {
                     case Constants.GET:
                         responseBody = request.PersonId == null 
                             ? _service.GetGroupGreeting() 
                             : _service.GetIndividualGreeting(request.PersonId);
                         _statusCode = Constants.StatusCodeOk;
                         break;
                     default:
                         return new Response(String.Empty, Constants.StatusCodeMethodNotAllowed);
                 }
             }
             catch (IdDoesNotExistException)
             {
                 return new Response(Messages.InvalidIdMessage, Constants.StatusCodeNotFound);
             }

             Response response = new Response(responseBody, _statusCode);
             
             return response;
         }
    }
}