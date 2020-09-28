using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HeroTrainerApp.API.Data;
using HeroTrainerApp.API.Dtos;
using HeroTrainerApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace HeroTrainerApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public AuthController(IAuthRepository repo, IConfiguration config, IMapper mapper)
        {
            _mapper = mapper;
            _config = config;
            _repo = repo;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(TrainerForRegisterDto trainerForRegisterDto)
        {

            trainerForRegisterDto.UserName = trainerForRegisterDto.UserName.ToLower();

            if (await _repo.TrainerExists(trainerForRegisterDto.UserName))
                return BadRequest("user name allready exists");

            var trainerToCreate = _mapper.Map<Trainer>(trainerForRegisterDto);

            var createdTrainer = await _repo.Register(trainerToCreate, trainerForRegisterDto.Password);

            var trainerToReturn = _mapper.Map<TrainerForDetailedDto>(createdTrainer);
            return CreatedAtRoute("GetTrainer", new { controller = "Trainers", id = createdTrainer.Id }, trainerToReturn);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(TrainerForLoginDto trainerForLoginDto)
        {
            var trainerFromRepo = await _repo.Login(trainerForLoginDto.Username.ToLower(), trainerForLoginDto.Password);
            if (trainerFromRepo == null)
                return Unauthorized();
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,trainerFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, trainerFromRepo.UserName)
                };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds

            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var trainer = _mapper.Map<TrainerForDetailedDto>(trainerFromRepo);
            return Ok(
                new
                {
                    token = tokenHandler.WriteToken(token),
                    trainer
                }
            );
        }
    }
}