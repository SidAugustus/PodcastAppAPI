using AutoMapper;
using PodcastApp.DTO;
using PodcastApp.Models;

namespace PodcastApp.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, RegisterRequestDTO>().ReverseMap()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.IsFlagged, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.IsSuspended, opt => opt.MapFrom(src => false));

            CreateMap<Podcast, PodcastUploadDTO>().ReverseMap();
            CreateMap<Episode, EpisodeDTO>().ReverseMap();
            CreateMap<Subscription, SubscriptionDTO>().ReverseMap();

        }
    }
}
