namespace BasicWebApp
{
    public class Request
    {
        public string HttpVerb;
        public string Body;
        public ControllerType ControllerType;
        public int? PersonId;

        public Request(string httpVerb, string body, ControllerType controllerType, int? personId)
        {
            HttpVerb = httpVerb;
            Body = body;
            ControllerType = controllerType;
            PersonId = personId;
        }
    }
}