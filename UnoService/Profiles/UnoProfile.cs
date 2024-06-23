using AutoMapper;
using UnoService.Dtos;
using UnoService.Models;

namespace UnoService.Profiles
{
    public class UnoProfile: Profile
    {
        public UnoProfile()
        {
            // Source -> Target
            CreateMap<Uno, UnoReadDto>();
            CreateMap<UnoCreateDto, Uno>();
            CreateMap<UnoReadDto, UnoPublishedDto>();
            CreateMap<Uno, GrpcUnoModel>()
                .ForMember(dest => dest.UnoId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher));

        }
    }
}