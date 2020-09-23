
using HeroTrainerApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HeroTrainerApp.API.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext>options):base(options){}
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Trainer> Trainers { get; set; }

    }
}