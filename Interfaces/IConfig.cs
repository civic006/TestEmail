namespace TestEmailService.Interfaces
{
    public interface IConfig
    {
        string GetUserName();
        string GetPassword();
        string GetName();
        string GetTo();
        string GetToUserName();
    }
}
