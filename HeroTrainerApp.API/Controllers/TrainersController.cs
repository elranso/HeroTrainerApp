using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HeroTrainerApp.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HeroTrainerApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TrainersController : ControllerBase
    {
        private readonly DataContext _context;
        public TrainersController(DataContext context)
        {
            _context = context;

        }
        [HttpGet]
        public async Task<IActionResult> GetTrainers()
        {
            var trainers = await _context.Trainers.ToListAsync();
            return Ok(trainers);
        }

        // GET api/Trainers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrainer(int id)
        {
            var trainer = await _context.Trainers.FirstOrDefaultAsync(t => t.Id == id);
            return Ok(trainer);
        }

    }
}