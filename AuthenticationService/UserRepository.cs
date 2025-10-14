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
                Login = "ivanov"
            },
            new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Петр",
                LastName = "Петров",
                Email = "petr@mail.ru",
                Password = "password123",
                Login = "petrov"
            },
            new User
            {
                Id = Guid.NewGuid(),
                FirstName = "Мария",
                LastName = "Сидорова",
                Email = "maria@yandex.ru",
                Password = "qwerty456",
                Login = "sidorova"
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