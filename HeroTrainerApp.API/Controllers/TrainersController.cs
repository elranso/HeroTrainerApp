using System.Threading.Tasks;
using AutoMapper;
using HeroTrainerApp.API.Data;
using HeroTrainerApp.API.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HeroTrainerApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TrainersController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly ITrainingRepository _repo;
        public TrainersController(ITrainingRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;

        }

        [HttpGet("{id}", Name = "GetTrainer")]
        public async Task<IActionResult> GetTrainer(int id)
        {
            var trainer = await _repo.GetTrainer(id);
            var trainerToReturn = _mapper.Map<TrainerForDetailedDto>(trainer);
            return Ok(trainerToReturn);
        }


    }
}