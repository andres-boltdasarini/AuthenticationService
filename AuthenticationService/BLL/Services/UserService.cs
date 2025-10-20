using AutoMapper;
using AuthenticationService.DAL.Models;
using AuthenticationService.DAL.Repositories;
using AuthenticationService.BLL.Interfaces;
using AuthenticationService.BLL.Models;
using AuthenticationService.BLL.Exceptions;

namespace AuthenticationService.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserService(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public IEnumerable<UserDto> GetAllUsers()
        {
            var users = _userRepository.GetAll();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        public UserDto GetUserByLogin(string login)
        {
            if (string.IsNullOrEmpty(login))
                throw new ArgumentException("Логин не может быть пустым");

            var user = _userRepository.GetByLogin(login);
            if (user == null)
                throw new AuthenticationException("Пользователь не найден");

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> AuthenticateAsync(string login, string password)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
                throw new ArgumentException("Логин и пароль обязательны");

            var user = _userRepository.GetByLogin(login);
            if (user == null)
                throw new AuthenticationException("Пользователь не найден");

            if (user.Password != password)
                throw new AuthenticationException("Неверный пароль");

            return _mapper.Map<UserDto>(user);
        }

        public UserDto GetUserViewModel()
        {
            var user = new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Иван",
                LastName = "Иванов",
                Email = "ivan@gmail.com",
                Password = "11111122222qq",
                Login = "ivanov",
                Role = new Role { Name = "Администратор" }
            };

            return _mapper.Map<UserDto>(user);
        }
    }
}