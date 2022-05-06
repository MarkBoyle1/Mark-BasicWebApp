namespace BasicWebApp
{
    public interface IServiceGreeting
    {
        public string GetGroupGreeting();
        public string GetIndividualGreeting(int? personId);
    }
}