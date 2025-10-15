using System.Collections.Generic;
using System.Linq;

namespace AuthenticationService
{
    public class UserRepository : IUserRepository
    {
        private readonly List<User> _users = new List<User>
        {
            new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Иван",
                LastName = "Иванов",
                Email = "ivan@gmail.com",
                Password = "11111122222qq",
                Login = "ivanov",
                Role = new Role { Id = 1, Name = "Admin" }
            },
            new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Петр",
                LastName = "Петров",
                Email = "petr@mail.ru",
                Password = "password123",
                Login = "petrov",
                Role = new Role { Id = 2, Name = "User" }
            },
            new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Мария",
                LastName = "Сидорова",
                Email = "maria@yandex.ru",
                Password = "qwerty456",
                Login = "sidorova",
                Role = new Role { Id = 2, Name = "User" }
            }
        };

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public User GetByLogin(string login)
        {
            return _users.FirstOrDefault(u => u.Login == login);
        }
    }
}