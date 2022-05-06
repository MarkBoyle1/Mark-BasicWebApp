using System;

namespace BasicWebApp
{
    public class Constants
    {
        public const string GET = "GET";
        public const string PUT = "PUT";
        public const string DELETE = "DELETE";
        public const string POST = "POST";
        public const string Greeting = "greeting";
        public const string Person = "person";

        public static string InitialPerson = Environment.GetEnvironmentVariable("INITIAL_NAME");

        public const int StatusCodeMethodNotAllowed = 405;
        public const int StatusCodesBadRequest = 400;
        public const int StatusCodeOk = 200;
        public const int StatusCodeCreated = 201;
        
        public static string[] prefixes = new string[]
        {
            "http://*:8080/person/",
            "http://*:8080/greeting/"
        };
    }
}