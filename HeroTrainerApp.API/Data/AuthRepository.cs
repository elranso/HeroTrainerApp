using System;
using System.Linq;
using System.Threading.Tasks;
using HeroTrainerApp.API.Models;
using Microsoft.EntityFrameworkCore;

namespace HeroTrainerApp.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;

        public AuthRepository(DataContext context)
        {
            _context = context;

        }

        public async Task<Trainer> Login(string username, string password)
        {
            var trainer = await _context.Trainers
            .Include(h => h.Heroes)
            .FirstOrDefaultAsync(t => t.UserName == username);
            trainer.Heroes = trainer.Heroes.OrderBy(h => h.CurrentPower).ToList();
            if (trainer == null)
                return null;

            if (!VerifyPasswordHash(password, trainer.PasswordHash, trainer.PasswordSalt))
                return null;
            return trainer;

        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {

                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }

        public async Task<Trainer> Register(Trainer trainer, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            trainer.PasswordHash = passwordHash;
            trainer.PasswordSalt = passwordSalt;

            await _context.Trainers.AddAsync(trainer);
            await _context.SaveChangesAsync();

            return trainer;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> TrainerExists(string username)
        {
            if (await _context.Trainers.AnyAsync(x => x.UserName == username))
                return true;
            return false;
        }
    }
}