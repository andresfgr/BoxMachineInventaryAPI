using BoxMachineInventary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoxMachineInventary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardbordPalletInventaryController : ControllerBase
    {
        private readonly BoxMachineInventaryContext _boxMachineInventaryContext;
        public CardbordPalletInventaryController(BoxMachineInventaryContext boxMachineInventaryContext)
        {
            _boxMachineInventaryContext = boxMachineInventaryContext;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<CardbordPalletInventary>>> GetCardbordPalletInventarys()
        {
            try
            {
                if (_boxMachineInventaryContext.CardbordPalletInventary == null)
                {
                    return NotFound();
                }

                return await _boxMachineInventaryContext.CardbordPalletInventary.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CardbordPalletInventary>> GetCardbordPalletInventary(int id)
        {

            if (_boxMachineInventaryContext.CardbordPalletInventary == null)
            {
                return NotFound();
            }

            var cardbordPalletInventary = await _boxMachineInventaryContext.CardbordPalletInventary.FindAsync(id);

            if (cardbordPalletInventary == null)
            {
                return NotFound();
            }

            return cardbordPalletInventary;
        }

        [HttpPost]
        public async Task<ActionResult<CardbordPalletInventary>> CreateCardbordPalletInventary(CardbordPalletInventary cardbordPalletInventary)
        {
            cardbordPalletInventary.EntryDate = DateTime.Now;
            //cardbordPalletInventary.UsedDate = DateTime.Now;
            cardbordPalletInventary.IsUsed = 0;
            _boxMachineInventaryContext.CardbordPalletInventary.Add(cardbordPalletInventary);

            try
            {
                await _boxMachineInventaryContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

            return CreatedAtAction(nameof(GetCardbordPalletInventary), new { id = cardbordPalletInventary.Id }, cardbordPalletInventary);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCardbordPalletInventary(int id)
        {
            if (_boxMachineInventaryContext.CardbordPalletInventary == null)
            {
                return NotFound();
            }

            var cardbordPalletInventary = await _boxMachineInventaryContext.CardbordPalletInventary.FindAsync(id);

            if (cardbordPalletInventary == null)
            {
                return NotFound();
            }

            cardbordPalletInventary.UsedDate = DateTime.Now;
            cardbordPalletInventary.IsUsed = 1;
            _boxMachineInventaryContext.Entry(cardbordPalletInventary).State = EntityState.Modified;

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
    }
}
