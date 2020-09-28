using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using HeroTrainerApp.API.Data;
using HeroTrainerApp.API.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace HeroTrainerApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        private readonly ITrainingRepository _repo;
        private readonly IMapper _mapper;
        public HeroesController(ITrainingRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }
        [HttpGet]
        public async Task<IActionResult> GetHeroes(int trainerId)
        {
            var heroes = await _repo.GetHeroes();
            var heroToReturn = _mapper.Map<IEnumerable<HeroForReturnDto>>(heroes);
            return Ok(heroToReturn); ;

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateHero(int id, HeroForUpdateDto heroForUpdateDto)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var heroFromRepo = await _repo.GetHero(id);

            _mapper.Map(heroForUpdateDto, heroFromRepo);
            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Updating hero {id} failed on save");
        }

    }
}