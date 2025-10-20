using AuthenticationService.BLL.Models;

namespace AuthenticationService.BLL.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetAllUsers();
        UserDto GetUserByLogin(string login);
        Task<UserDto> AuthenticateAsync(string login, string password);
        UserDto GetUserViewModel();
    }
}