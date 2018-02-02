namespace DatingApp.API.Helpers
{
    public interface IUserSession
    {
         string UserId { get; }
         string UserIP { get; }

    }
}