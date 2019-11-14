using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MDAProject.Web.Data;
using MDAProject.Web.Data.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MDAProject.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DeviceTypesController : ControllerBase
    {
        private readonly DataContext _context;

        public DeviceTypesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/DeviceTypes
        [HttpGet]
        public IEnumerable<DeviceType> GetDeviceTypes()
        {
            return _context.DeviceTypes.OrderBy(dt => dt.DeviceTypeName);
        }

        // GET: api/DeviceTypes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDeviceType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deviceType = await _context.DeviceTypes.FindAsync(id);

            if (deviceType == null)
            {
                return NotFound();
            }

            return Ok(deviceType);
        }

        // PUT: api/DeviceTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeviceType([FromRoute] int id, [FromBody] DeviceType deviceType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != deviceType.Id)
            {
                return BadRequest();
            }

            _context.Entry(deviceType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceTypeExists(id))
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

        // POST: api/DeviceTypes
        [HttpPost]
        public async Task<IActionResult> PostDeviceType([FromBody] DeviceType deviceType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.DeviceTypes.Add(deviceType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDeviceType", new { id = deviceType.Id }, deviceType);
        }

        // DELETE: api/DeviceTypes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeviceType([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var deviceType = await _context.DeviceTypes.FindAsync(id);
            if (deviceType == null)
            {
                return NotFound();
            }

            _context.DeviceTypes.Remove(deviceType);
            await _context.SaveChangesAsync();

            return Ok(deviceType);
        }

        private bool DeviceTypeExists(int id)
        {
            return _context.DeviceTypes.Any(e => e.Id == id);
        }
    }
}