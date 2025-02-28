
using AutoMapper;
using ChatProject.API.DTOs;
using ChatProject.Domain.Entities;
using Profile = ChatProject.Domain.Entities.Profile;
namespace ChatProject.API.Mappings
{
    public class MappingProfile:AutoMapper.Profile
    {
        public MappingProfile()
        {
            // Map from RegisterDto to ApplicationUser (Only username and email)
            CreateMap<RegisterDto, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Username))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

            // Map from ProfileDto to Profile entity
            CreateMap<ProfileDto, Profile>()
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src => src.ProfilePictureUrl))
                .ForMember(dest => dest.Bio, opt => opt.MapFrom(src => src.Bio))
                .ForMember(dest => dest.Birthdate, opt => opt.MapFrom(src => src.Birthdate))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.Company))
                .ForMember(dest => dest.Website, opt => opt.MapFrom(src => src.Website));

            // Map from Profile entity to ProfileDto
            CreateMap<Profile, ProfileDto>();
        }
    }
}
