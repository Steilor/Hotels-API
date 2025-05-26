namespace Hotelss.Application.User
{
    public interface IUserContext
    {
        CurrentUser? GetCurrentUser();
    }
}