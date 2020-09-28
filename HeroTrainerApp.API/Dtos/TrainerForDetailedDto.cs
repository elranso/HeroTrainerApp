using System.Collections.Generic;
using HeroTrainerApp.API.Models;

namespace HeroTrainerApp.API.Dtos
{
    public class TrainerForDetailedDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public ICollection<Hero> Heroes { get; set; }
    }
}