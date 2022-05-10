using System.IO;
using System.Net;
using System.Text;

namespace BasicWebApp
{
    public class Response
    {
        public string Body;
        public int StatusCode;
        public Response(string responseBody, int statusCode)
        {
            Body = responseBody;
            StatusCode = statusCode;
        }

        public void Send(HttpListenerContext context)
        {
            var buffer = Encoding.UTF8.GetBytes(Body);
            context.Response.ContentLength64 = buffer.Length;
            context.Response.StatusCode = StatusCode;
            
            Stream output = context.Response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }
    }
}