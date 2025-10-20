using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AuthenticationService.BLL.Interfaces;
using AuthenticationService.BLL.Models;

namespace AuthenticationService.PLL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> GetUsers()
        {
            var users = _userService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{login}")]
        public ActionResult<UserDto> GetUserByLogin(string login)
        {
            try
            {
                var user = _userService.GetUserByLogin(login);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Пользователь с логином {Login} не найден", login);
                return NotFound($"Пользователь с логином {login} не найден");
            }
        }

        [Authorize(Roles = "Администратор")]
        [HttpGet("viewmodel")]
        public ActionResult<UserDto> GetUserViewModel()
        {
            try
            {
                var userViewModel = _userService.GetUserViewModel();
                return Ok(userViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении UserViewModel");
                return StatusCode(500, "Внутренняя ошибка сервера");
            }
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<UserDto>> Authenticate(string login, string password)
        {
            try
            {
                var user = await _userService.AuthenticateAsync(login, password);

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, login),
                    new Claim(ClaimTypes.Role, user.RoleName)
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                _logger.LogInformation("Пользователь {Login} успешно аутентифицирован", login);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Ошибка аутентификации для пользователя {Login}", login);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _logger.LogInformation("Пользователь вышел из системы");
            return Ok();
        }
    }
}