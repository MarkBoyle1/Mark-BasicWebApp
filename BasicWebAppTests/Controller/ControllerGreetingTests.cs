using System;
using BasicWebApp;
using BasicWebApp.Controllers;
using Moq;
using Xunit;

namespace BasicWebAppTests
{
    public class ControllerGreetingTests
    {
        [Fact]
        public void given_HttpVerbEqualsGET_and_NoIdIsGiven_when_ProcessRequest_then_call_GetGroupGreeting()
        {
            var mockService = new Mock<IServiceGreeting>();
            
            ControllerGreeting controllerGreeting =
                new ControllerGreeting(mockService.Object);

            int? id = null;
            string httpVerb = Constants.GET;
            string body = String.Empty;
            ControllerType controllerType = ControllerType.Greeting;
        
            Request request = new Request(httpVerb, body, controllerType, id);
        
            controllerGreeting.ProcessRequest(request);
            
            mockService.Verify(mock => mock.GetGroupGreeting(), Times.Once);
        }
        
        [Fact]
        public void given_HttpVerbEqualsGET_and_ValidId_when_ProcessRequest_then_call_GetIndividualGreeting()
        {
            var mockService = new Mock<IServiceGreeting>();
            
            ControllerGreeting controllerGreeting =
                new ControllerGreeting(mockService.Object);

            int? id = 1;
            string httpVerb = Constants.GET;
            string body = String.Empty;
            ControllerType controllerType = ControllerType.Greeting;
        
            Request request = new Request(httpVerb, body, controllerType, id);
        
            controllerGreeting.ProcessRequest(request);
            
            mockService.Verify(mock => mock.GetIndividualGreeting(id), Times.Once);
        }
        
        [Theory]
        [InlineData(Constants.POST)]
        [InlineData(Constants.PUT)]
        [InlineData(Constants.DELETE)]
        public void given_HttpVerbDoesNotEqualGET_when_ProcessRequest_then_return_ResponseWithMethodNotAllowedStatusCode(string httpVerb)
        {
            var mockService = new Mock<IServiceGreeting>();
            
            ControllerGreeting controllerGreeting =
                new ControllerGreeting(mockService.Object);

            int? id = 1;
            string body = String.Empty;
            ControllerType controllerType = ControllerType.Greeting;
        
            Request request = new Request(httpVerb, body, controllerType, id);
        
            Response response = controllerGreeting.ProcessRequest(request);
            
            Assert.Equal(Constants.StatusCodeMethodNotAllowed, response.StatusCode);
        }
    }
}
