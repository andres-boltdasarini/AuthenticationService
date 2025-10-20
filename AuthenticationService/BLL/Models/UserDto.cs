namespace AuthenticationService.BLL.Models
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public bool FromRussia { get; set; }
        public string RoleName { get; set; }
    }
}