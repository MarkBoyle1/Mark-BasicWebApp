using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using BasicWebApp.Exceptions;

namespace BasicWebApp
{
    public class RequestParser
    {
        public Request ParseRequest(HttpListenerContext context)
        {
            string url = context.Request.Url.ToString();
            string httpVerb = context.Request.HttpMethod;
            string requestBody = new StreamReader(context.Request.InputStream).ReadToEnd();

            string[] splitUrl = url.Split('/');
            
            //Index 3 contains the controller type (Greeting or Person)
            string controllerType = splitUrl.Length > 3 ? splitUrl[3] : null;
            
            int? personId = int.TryParse(splitUrl.Last(), out int _) ? Convert.ToInt16(splitUrl.Last()) : null;

            return new Request(httpVerb, requestBody, DetermineControllerType(controllerType), personId);
        }

        public ControllerType DetermineControllerType(string controllerType)
        {
            switch (controllerType)
            {
                case Constants.Greeting:
                    return ControllerType.Greeting;
                case Constants.Person:
                    return ControllerType.Person;
                default:
                    throw new InvalidControllerTypeException();
            }
        }
    }
}