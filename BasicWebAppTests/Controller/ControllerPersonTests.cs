using System;
using System.Collections.Generic;
using BasicWebApp;
using BasicWebApp.Database;
using BasicWebApp.DTO;
using BasicWebApp.Services;
using Moq;
using Xunit;

namespace BasicWebAppTests
{
    public class ControllerPersonTests
    {
        [Fact]
        public void given_HttpVerbEqualsGET_and_NoIdGiven_when_ProcessRequest_then_GetPersonListIsCalledOnce()
        {
            var mockService = new  Mock<IServicePerson>();
            var mockDTOGenerator = new Mock<IDTOGenerator>();
            
            ControllerPerson controllerPerson = new ControllerPerson(mockService.Object, mockDTOGenerator.Object);
            
            string httpVerb = Constants.GET;
            string body = String.Empty;
            ControllerType controllerType= ControllerType.Person;
        
            Request request = new Request(httpVerb, body, controllerType, null);
        
            controllerPerson.ProcessRequest(request);
            
            mockService.Verify(mock => mock.GetPersonList(), Times.Once);
        }
        
        [Fact]
        public void given_HttpVerbEqualsGET_and_IdEqualsOne_when_ProcessRequest_then_GetPersonInfoIsCalledOnce()
        {
            int? id = 1;
            var mockService = new  Mock<IServicePerson>();
            var mockDTOGenerator = new Mock<IDTOGenerator>();
            
            ControllerPerson controllerPerson = new ControllerPerson(mockService.Object, mockDTOGenerator.Object);
            
            string httpVerb = Constants.GET;
            string body = String.Empty;
            ControllerType controllerType= ControllerType.Person;
        
            Request request = new Request(httpVerb, body, controllerType, id);
        
            controllerPerson.ProcessRequest(request);
            
            mockService.Verify(mock => mock.GetPersonInfo(id), Times.Once);
        }
        
        [Fact]
        public void given_HttpVerbEqualsPOST_when_ProcessRequest_then_AddPersonIsCalledOnce()
        {
            int? id = null;
            string httpVerb = Constants.POST;
            ControllerType controllerType= ControllerType.Person;
            string requestBody = "{\"Name\": \"Bob\"}";
            
            Request request = new Request(httpVerb, requestBody, controllerType, id);

            PersonDTO addPersonDto = new PersonDTO(){Name = "Bob"};

            var mockDTOGenerator = new Mock<IDTOGenerator>();
            mockDTOGenerator.Setup(mock => mock.GeneratePersonDTO(requestBody, null)).Returns(addPersonDto);
            
            var mockService = new  Mock<IServicePerson>();

            ControllerPerson controllerPerson = new ControllerPerson(mockService.Object, mockDTOGenerator.Object);

            controllerPerson.ProcessRequest(request);
            
            mockService.Verify(mock => mock.AddPerson(addPersonDto), Times.Once);
        }
        
        [Fact]
        public void given_HttpVerbEqualsDELETE_when_ProcessRequest_then_DeletePersonIsCalledOnce()
        {
            int? id = 1;
            string httpVerb = Constants.DELETE;
            string requestBody = String.Empty;
            ControllerType controllerType= ControllerType.Person;

            IDTOGenerator dtoGenerator = new DTOGenerator();
            var mockService = new  Mock<IServicePerson>();

            ControllerPerson controllerPerson = new ControllerPerson(mockService.Object, dtoGenerator);
            
            Request request = new Request(httpVerb, requestBody, controllerType, id);
        
            controllerPerson.ProcessRequest(request);
            
            mockService.Verify(mock => mock.DeletePerson(id), Times.Once);
        }
        
        [Fact]
        public void given_HttpVerbEqualsPUT_when_ProcessRequest_then_UpdatePersonIsCalledOnce()
        {
            int? id = 1;
            string httpVerb = Constants.PUT;
            string requestBody = "{\"Name\": \"Bob\"}";
            ControllerType controllerType= ControllerType.Person;

            PersonDTO personDto = new PersonDTO();
            personDto.Id = id;
            personDto.Name = "Bob";
            
            var mockService = new Mock<IServicePerson>();
            var mockDTOGenerator = new Mock<IDTOGenerator>();
            mockDTOGenerator.Setup(mock => mock.GeneratePersonDTO(requestBody, id)).Returns(personDto);

        
            ControllerPerson controllerPerson = new ControllerPerson(mockService.Object, mockDTOGenerator.Object);
            
            Request request = new Request(httpVerb, requestBody, controllerType, id);
        
            controllerPerson.ProcessRequest(request);
            
            mockService.Verify(mock => mock.UpdatePerson(personDto), Times.Once);
        }

        [Fact]
        public void
            given_HttpVerbEqualsPOST_and_BodyIsEmpty_when_ProcessRequest_then_return_ResponseWithBadRequestCode()
        {
            IDatabase mockDatabase = new MockDatabase();
            IServicePerson servicePerson = new ServicePerson(mockDatabase);
            IDTOGenerator dtoGenerator = new DTOGenerator();

            ControllerPerson controllerPerson = new ControllerPerson(servicePerson, dtoGenerator);
            
            string httpVerb = Constants.POST;
            string body = String.Empty;
            ControllerType controllerType = ControllerType.Person;
        
            Request request = new Request(httpVerb, body, controllerType, null);
        
            Response response = controllerPerson.ProcessRequest(request);
            
            Assert.Equal("Error: Request body is empty.", response.Body);
            Assert.Equal(Constants.StatusCodeBadRequest, response.StatusCode);
        }
        
        [Fact]
        public void
            given_HttpVerbEqualsPOST_and_BodyIsInvalidData_when_ProcessRequest_then_return_ResponseWithBadRequestCode()
        {
            IDatabase mockDatabase = new MockDatabase();
            IServicePerson servicePerson = new ServicePerson(mockDatabase);
            IDTOGenerator dtoGenerator = new DTOGenerator();

            ControllerPerson controllerPerson = new ControllerPerson(servicePerson, dtoGenerator);
            
            string httpVerb = Constants.POST;
            string body = "MockInvalidData";
            ControllerType controllerType = ControllerType.Person;
        
            Request request = new Request(httpVerb, body, controllerType, null);
        
            Response response = controllerPerson.ProcessRequest(request);
            
            Assert.Equal("Error: Invalid request body.", response.Body);
            Assert.Equal(Constants.StatusCodeBadRequest, response.StatusCode);
        }
        
        [Fact]
        public void
            given_HttpVerbEqualsDELETE_and_IdEqualsOne_when_ProcessRequest_then_return_StatusCode200_and_EmptyBody()
        {
            IDatabase mockDatabase = new MockDatabase();
            mockDatabase.AddPerson(new Person("Mark"));
            IServicePerson servicePerson = new ServicePerson(mockDatabase);
            IDTOGenerator dtoGenerator = new DTOGenerator();

            ControllerPerson controllerPerson = new ControllerPerson(servicePerson, dtoGenerator);
            
            string httpVerb = Constants.DELETE;
        
            ControllerType controllerType = ControllerType.Person;
        
            Request request = new Request(httpVerb, String.Empty, controllerType, 1);
        
            Response response = controllerPerson.ProcessRequest(request);
            
            Assert.Equal(200, response.StatusCode);
            Assert.Empty(response.Body);
        }
        
        [Fact]
        public void
            given_HttpVerbEqualsDELETE_and_IdDoesNotExist_when_ProcessRequest_then_return_StatusCode400_and_ErrorMessage()
        {
            IDatabase mockDatabase = new MockDatabase();
            IServicePerson servicePerson = new ServicePerson(mockDatabase);
            IDTOGenerator dtoGenerator = new DTOGenerator();
            ControllerPerson controllerPerson = new ControllerPerson(servicePerson, dtoGenerator);
            
            string httpVerb = Constants.DELETE;
        
            ControllerType controllerType = ControllerType.Person;
        
            Request request = new Request(httpVerb, String.Empty, controllerType, 2);
        
            Response response = controllerPerson.ProcessRequest(request);
            
            Assert.Equal(404, response.StatusCode);
            Assert.Equal("Error: Id does not exist.", response.Body);
        }
        
        [Fact]
        public void
            given_HttpVerbEqualsPUT_and_BodyIsEmpty_when_ProcessRequest_then_return_ResponseWithBadRequestCode()
        {
            IDatabase mockDatabase = new MockDatabase();
            IServicePerson servicePerson = new ServicePerson(mockDatabase);
            IDTOGenerator dtoGenerator = new DTOGenerator();

            ControllerPerson controllerPerson = new ControllerPerson(servicePerson, dtoGenerator);
            
            string httpVerb = Constants.PUT;
            string body = String.Empty;
            ControllerType controllerType = ControllerType.Person;
        
            Request request = new Request(httpVerb, body, controllerType, 1);
        
            Response response = controllerPerson.ProcessRequest(request);
            
            Assert.Equal("Error: Request body is empty.", response.Body);
            Assert.Equal(Constants.StatusCodeBadRequest, response.StatusCode);
        }
        
        [Fact]
        public void
            given_HttpVerbEqualsPUT_and_BodyIsInvalidData_when_ProcessRequest_then_return_ResponseWithBadRequestCode()
        {
            IDatabase mockDatabase = new MockDatabase();
            IServicePerson servicePerson = new ServicePerson(mockDatabase);
            IDTOGenerator dtoGenerator = new DTOGenerator();
            ControllerPerson controllerPerson = new ControllerPerson(servicePerson, dtoGenerator);
        
            string httpVerb = Constants.PUT;
            string body = "MockInvalidData";
            ControllerType controllerType = ControllerType.Person;
        
            Request request = new Request(httpVerb, body, controllerType, 1);
        
            Response response = controllerPerson.ProcessRequest(request);
            
            Assert.Equal("Error: Invalid request body.", response.Body);
            Assert.Equal(Constants.StatusCodeBadRequest, response.StatusCode);
        }
        
        [Fact]
        public void given_HttpVerbEqualsPATCH_when_ProcessRequest_then_return_MethodNotAllowed()
        {
            string httpVerb = "PATCH";
            string body = String.Empty;
            ControllerType controllerType= ControllerType.Person;
        
            IDatabase mockDatabase = new MockDatabase();
            IServicePerson servicePerson = new ServicePerson(mockDatabase);
            IDTOGenerator dtoGenerator = new DTOGenerator();
            ControllerPerson controllerPerson = new ControllerPerson(servicePerson, dtoGenerator);
            
            Request request = new Request(httpVerb, body, controllerType, 1);
        
            Response response = controllerPerson.ProcessRequest(request);
            
            Assert.Equal(Constants.StatusCodeMethodNotAllowed, response.StatusCode);
        }
    }
}