namespace BasicWebApp.DTO
{
    public interface IDTOGenerator
    {
        public PersonDTO GeneratePersonDTO(string requestBody, int? id = null);
    }
}