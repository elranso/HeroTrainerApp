using HeroTrainerApp.API.Dtos;
using HeroTrainerApp.API.Models;
using AutoMapper;

namespace HeroTrainerApp.API.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<TrainerForRegisterDto, Trainer>();
            CreateMap<Trainer, TrainerForDetailedDto>();
            CreateMap<HeroForUpdateDto, Hero>();
            CreateMap<Hero, HeroForReturnDto>();
        }
    }
}