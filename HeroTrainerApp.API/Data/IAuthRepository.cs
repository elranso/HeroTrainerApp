using System.Threading.Tasks;
using HeroTrainerApp.API.Models;

namespace HeroTrainerApp.API.Data
{
    public interface IAuthRepository
    {
         
         Task<Trainer> Register(Trainer trainer, string password);
         Task<Trainer> Login(string username, string password);
         Task<bool> TrainerExists(string username);
    }
}