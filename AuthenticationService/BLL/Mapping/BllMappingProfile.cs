using AutoMapper;
using AuthenticationService.DAL.Models;
using AuthenticationService.BLL.Models;

namespace AuthenticationService.BLL.Mapping
{
    public class BllMappingProfile : Profile
    {
        public BllMappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
                .ForMember(dest => dest.FromRussia, opt => opt.MapFrom(src => IsRussianEmail(src.Email)))
                .ForMember(dest => dest.RoleName, opt => opt.MapFrom(src => src.Role.Name));
        }

        private static bool IsRussianEmail(string email)
        {
            return email.ToLower().Contains(".ru");
        }
    }
}