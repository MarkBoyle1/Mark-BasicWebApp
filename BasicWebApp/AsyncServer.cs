using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using BasicWebApp.Controllers;
using BasicWebApp.Database;
using BasicWebApp.DTO;
using BasicWebApp.Exceptions;
using BasicWebApp.Services;

namespace BasicWebApp
{
    public class AsyncServer
    {
        private ControllerGreeting _controllerGreeting;
        private ControllerPerson _controllerPerson;
        private IController _controller;
        private RequestParser _requestParser;

        public AsyncServer()
        {
            IDatabase database = new MockDatabase();
            database.AddPerson(new Person(Constants.InitialPerson));
            _controllerGreeting = new ControllerGreeting(new ServiceGreeting(database));
            _controllerPerson = new ControllerPerson(new ServicePerson(database), new DTOGenerator());
            _requestParser = new RequestParser();
        }

        public void RunServer()
        {
            HttpListener listener = new HttpListener();
            
            foreach (string prefix in Constants.prefixes)
            {
                listener.Prefixes.Add(prefix);
            }

            listener.Start();
            
            while (true)
            {
                IAsyncResult result = listener.BeginGetContext(new AsyncCallback(ListenerCallback),listener);
                result.AsyncWaitHandle.WaitOne();
            }
            
            listener.Close();
        }

        private void ListenerCallback(IAsyncResult result)
        {
            HttpListener listener = (HttpListener) result.AsyncState;
            
            // Call EndGetContext to complete the asynchronous operation.
            HttpListenerContext context = listener.EndGetContext(result);
            
            Console.WriteLine($"{context.Request.HttpMethod} {context.Request.Url}");
            
            Response response;

            try
            {
                Request request = _requestParser.ParseRequest(context);

                _controller = SetController(request);
                response = _controller.ProcessRequest(request);
            }
            catch (InvalidControllerException)
            {
                response = new Response(String.Empty, Constants.StatusCodeNotFound);
            }
            
            var buffer = Encoding.UTF8.GetBytes(response.Body);

            // Get a response stream and write the response to it.
            context.Response.ContentLength64 = buffer.Length;
            context.Response.StatusCode = response.StatusCode;
            Stream output = context.Response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            
            // You must close the output stream.
            output.Close();
        }

        private IController SetController(Request request)
        {
            switch (request.ControllerType)
            {
                case ControllerType.Greeting:
                    return _controllerGreeting;
                case ControllerType.Person:
                    return _controllerPerson;
                default:
                    throw new InvalidControllerException();
            }
        }
    }
}