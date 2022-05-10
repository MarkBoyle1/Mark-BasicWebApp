using BasicWebApp.DTO;
using Xunit;

namespace BasicWebAppTests
{
    public class DTOGeneratorTests
    {
        [Fact]
        public void
            given_RequestBodyContainsNameEqualsBob_when_GenerateAddPersonDTO_then_return_AddPersonDTOWhereNameEqualsBob()
        {
            string requestBody = "{\"Name\": \"Bob\"}";
            
            IDTOGenerator dtoGenerator = new DTOGenerator();

            PersonDTO addPersonDto = dtoGenerator.GeneratePersonDTO(requestBody);
            
            Assert.Equal("Bob", addPersonDto.Name);
        }
        
        [Fact]
        public void
            given_RequestBodyContainsNameEqualsBob_and_IdEqualsOne_when_GenerateUpdatedPersonDTO_then_return_UpdatedPersonDTOWhereNameEqualsBobAndIdEqualsOne()
        {
            int? id = 1;
            string requestBody = "{\"Name\": \"Bob\"}";
            
            IDTOGenerator dtoGenerator = new DTOGenerator();

            PersonDTO personDto = dtoGenerator.GeneratePersonDTO(requestBody, id);
            
            Assert.Equal("Bob", personDto.Name);
            Assert.Equal(1, personDto.Id);
        }
    }
}