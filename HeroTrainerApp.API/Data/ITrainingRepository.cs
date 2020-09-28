using System.Collections.Generic;
using System.Threading.Tasks;
using HeroTrainerApp.API.Models;

namespace HeroTrainerApp.API.Data
{
    public interface ITrainingRepository
    {
        Task<Trainer> GetTrainer(int id);
        Task<Hero> GetHero(int id);
        Task<IEnumerable<Hero>> GetHeroes();
        Task<bool> SaveAll();
    }
}