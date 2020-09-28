using System.Collections.Generic;
using System.Linq;
using HeroTrainerApp.API.Models;
using Newtonsoft.Json;

namespace HeroTrainerApp.API.Data
{
    public static class Seed
    {


        public static void SeedTrainers(DataContext context)
        {
            if (!context.Trainers.Any())
            {
                var trainerData = System.IO.File.ReadAllText("Data/TrainerSeedData.json");
                var trainers = JsonConvert.DeserializeObject<List<Trainer>>(trainerData);
                foreach (var trainer in trainers)
                {
                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash("Admin*123", out passwordHash, out passwordSalt);

                    trainer.PasswordHash = passwordHash;
                    trainer.PasswordSalt = passwordSalt;
                    trainer.UserName = trainer.UserName.ToLower();

                    context.Trainers.Add(trainer);
                }
            }

            context.SaveChanges();
        }

        public static void SeedHeroes(DataContext context)
        {
            if (!context.Heroes.Any())
            {
                var heroData = System.IO.File.ReadAllText("Data/HeroSeedData.json");
                var heroes = JsonConvert.DeserializeObject<List<Hero>>(heroData);
                foreach (var hero in heroes)
                {
                    context.Heroes.Add(hero);
                }
            }

            context.SaveChanges();
        }

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}