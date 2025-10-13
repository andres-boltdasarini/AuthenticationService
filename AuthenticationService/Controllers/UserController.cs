using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationService.Controllers
{
    [ApiController]
    [Route("[controller]")]


    public class UserController : ControllerBase
    {
        private readonly ILogger _logger;

        public UserController(ILogger logger) // Должен принимать ILogger, а не Logger
        {
            _logger = logger;
        

        logger.WriteEvent("evn");

            logger.WriteError("err");
        }
    


    [HttpGet]
        public User GetUser() {
            return new User()
            {
                Id = Guid.NewGuid(),
                FirstName = "Ivan",
                LastName = "Smirnov",
                Email = "ivan@mai.ru",
                Password = "1234",
                Login = "smirnov"
            };
        }
    }
}
