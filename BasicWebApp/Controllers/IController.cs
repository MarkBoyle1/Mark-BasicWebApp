namespace BasicWebApp
{
    public interface IController
    {
        public Response ProcessRequest(Request request);
    }
}