namespace Hotelss.Application.Users
{
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
    }
}