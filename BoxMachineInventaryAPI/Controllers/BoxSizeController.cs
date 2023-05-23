using BoxMachineInventaryAPI.Controllers;
using BoxMachineInventary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace BoxMachineInventary.Controllers
{
    [Route("api/[controller]"), Authorize(Roles = "Admin")]
    [ApiController]
    public class BoxSizeController : ControllerBase
    {
        private readonly ILogger<BoxSizeController> _logger;
        private readonly BoxMachineInventaryContext _boxMachineInventaryContext;
        public BoxSizeController(ILogger<BoxSizeController> logger, BoxMachineInventaryContext boxMachineInventaryContext)
        {
            _logger = logger;
            _boxMachineInventaryContext = boxMachineInventaryContext;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<BoxSize>>> GetBoxSizes()
        {
            if (_boxMachineInventaryContext.BoxSize == null)
            {
                return NotFound();
            }

            return await _boxMachineInventaryContext.BoxSize.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BoxSize>> GetBoxSize(int id)
        {
            if (_boxMachineInventaryContext.BoxSize == null)
            {
                return NotFound();
            }

            var boxSize = await _boxMachineInventaryContext.BoxSize.FindAsync(id);

            if (boxSize == null)
            {
                return NotFound();
            }

            return boxSize;
        }

        [HttpPost]
        public async Task<ActionResult<BoxSize>> CreateBoxSize(BoxSize boxSize)
        {
            _boxMachineInventaryContext.BoxSize.Add(boxSize);

            try
            {
                await _boxMachineInventaryContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

            return CreatedAtAction(nameof(GetBoxSize), new { id = boxSize.Id }, boxSize);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBoxSize(int id, BoxSize boxSize)
        {
            if (id != boxSize.Id)
            {
                return BadRequest();
            }

            _boxMachineInventaryContext.Entry(boxSize).State = EntityState.Modified;

            try
            {
                await _boxMachineInventaryContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBoxSize(int id)
        {
            if (_boxMachineInventaryContext.BoxSize == null)
            {
                return NotFound();
            }

            var boxSize = await _boxMachineInventaryContext.BoxSize.FindAsync(id);

            if (boxSize == null)
            {
                return NotFound();
            }

            _boxMachineInventaryContext.BoxSize.Remove(boxSize);
            await _boxMachineInventaryContext.SaveChangesAsync();

            return Ok();
        }
    }
}
