using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BagAPI.Data;
using BagAPI.Models;

namespace BagAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BagsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BagsController(ApplicationDbContext context)
        {
            _context = context;
        }

		//Method to retrieve all bags
        // GET: api/Bags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bag>>> GetBags()
        {
			
            if (_context.Bags == null)
            {
                return NotFound();
            }

            var bags = await _context.Bags.ToListAsync();
            return Ok(bags);
			
        }

		//	//Method to filter and retrieve bags with a given name. The search should be case-insensitive and should be for partial/full match
        // GET: api/Bags/"duffle"
        [HttpGet("{name}")]
        public async Task<ActionResult<IEnumerable<Bag>>> SearchBags(string name)
        {
			
            if (_context.Bags == null)
            {

                return NotFound();
            }
            var bags = await _context.Bags.Where(b => b.Name.ToLower().Contains(name.ToLower())).ToListAsync();

            if (bags.Count==0)
            {
                return NotFound();
            }
            else

            return Ok(bags);
            return null;
			
        }
        //Returns a Bag with the exact weight given which is the first occurence in the list
        [HttpGet("search")]
        public async Task<IActionResult> GetBagByWeightAsync(double weight)
        {
			
            var bag = await _context.Bags.FirstOrDefaultAsync(b => b.Weight == weight);

            if (bag == null)
            {
                return NotFound(); // No matching bag found
            }

            return Ok(bag);
			
        }

        

        
    }
}
