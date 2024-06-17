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

        }
    }
}