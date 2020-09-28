using System;

namespace HeroTrainerApp.API.Dtos
{
    public class HeroForUpdateDto
    {
        public int TrainerId { get; set; }
        public string Name { get; set; }
        public string Ability { get; set; }
        public DateTime TrainingStartDay { get; set; }
        public string SuitColor { get; set; }
        public decimal StartingPower { get; set; }
        public decimal CurrentPower { get; set; }
    }
}