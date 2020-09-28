using System.ComponentModel.DataAnnotations;

namespace HeroTrainerApp.API.Dtos
{
    public class TrainerForRegisterDto
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 8, ErrorMessage = "password must be at least characters")]
        public string Password { get; set; }

    }
}