using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MDAProject.Web.Data;
using MDAProject.Web.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace MDAProject.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MovementTypesController : ControllerBase
    {
        private readonly DataContext _context;

        public MovementTypesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/MovementTypes
        [HttpGet]
        public IEnumerable<MovementType> GetMovementTypes()
        {
            return _context.MovementTypes.OrderBy(mt => mt.MovementTypeName);
        }

        // GET: api/MovementTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovementType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movementType = await _context.MovementTypes.FindAsync(id);

            if (movementType == null)
            {
                return NotFound();
            }

            return Ok(movementType);
        }

        // PUT: api/MovementTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovementType([FromRoute] int id, [FromBody] MovementType movementType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != movementType.Id)
            {
                return BadRequest();
            }

            _context.Entry(movementType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovementTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MovementTypes
        [HttpPost]
        public async Task<IActionResult> PostMovementType([FromBody] MovementType movementType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.MovementTypes.Add(movementType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovementType", new { id = movementType.Id }, movementType);
        }

        // DELETE: api/MovementTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovementType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movementType = await _context.MovementTypes.FindAsync(id);
            if (movementType == null)
            {
                return NotFound();
            }

            _context.MovementTypes.Remove(movementType);
            await _context.SaveChangesAsync();

            return Ok(movementType);
        }

        private bool MovementTypeExists(int id)
        {
            return _context.MovementTypes.Any(e => e.Id == id);
        }
    }
}