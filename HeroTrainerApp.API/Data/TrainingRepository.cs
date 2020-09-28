using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeroTrainerApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HeroTrainerApp.API.Data
{
    public class TrainingRepository : ITrainingRepository
    {
        private readonly DataContext _context;
        public TrainingRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Hero> GetHero(int id)
        {
            var hero = await _context.Heroes.FirstOrDefaultAsync(h => h.Id == id);
            return hero;
        }

        public async Task<IEnumerable<Hero>> GetHeroes()
        {
            var heroes = await _context.Heroes.OrderBy(p => p.CurrentPower).ToListAsync();
            return heroes;
        }

        public async Task<Trainer> GetTrainer(int id)
        {

            var trainer = await _context.Trainers.Include(h => h.Heroes).FirstOrDefaultAsync(t => t.Id == id);
            trainer.Heroes = trainer.Heroes.OrderBy(h => h.CurrentPower).ToList();

            return trainer;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}