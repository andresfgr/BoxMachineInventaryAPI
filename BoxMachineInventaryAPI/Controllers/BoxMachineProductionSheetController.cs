using BoxMachineInventary.EntityModels;
using BoxMachineInventary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoxMachineInventary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoxMachineProductionSheetController : ControllerBase
    {
        private readonly BoxMachineInventaryContext _boxMachineInventaryContext;
        public BoxMachineProductionSheetController(BoxMachineInventaryContext boxMachineInventaryContext)
        {
            _boxMachineInventaryContext = boxMachineInventaryContext;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<BoxMachineProductionSheetModel>>> GetBoxMachineProductionSheets()
        {
            if (_boxMachineInventaryContext.BoxMachineProductionSheet == null)
            {
                return NotFound();
            }

            List<BoxMachineProductionSheetModel> boxMachineProductionSheetsModel = new List<BoxMachineProductionSheetModel>();
            var boxMachineProductionSheetList = await _boxMachineInventaryContext.BoxMachineProductionSheet.ToListAsync(); 
            var boxSizeList = await _boxMachineInventaryContext.BoxSize.ToListAsync();

            foreach (var item in boxMachineProductionSheetList)
            {
                BoxMachineProductionSheetModel boxMachineProductionSheetModel = new BoxMachineProductionSheetModel {
                    Id = item.Id,
                    BoxSizeId = item.BoxSizeId,
                    ProducedDate = item.ProducedDate,
                    ProducedQuantity = item.ProducedQuantity,
                    BoxSizeName = boxSizeList.Where(x => x.Id == item.BoxSizeId).First().Code
                };
                boxMachineProductionSheetsModel.Add(boxMachineProductionSheetModel);
            }

            return boxMachineProductionSheetsModel; 
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BoxMachineProductionSheet>> GetBoxMachineProductionSheet(int id)
        {
            if (_boxMachineInventaryContext.BoxMachineProductionSheet == null)
            {
                return NotFound();
            }

            var boxMachineProductionSheet = await _boxMachineInventaryContext.BoxMachineProductionSheet.FindAsync(id);

            if (boxMachineProductionSheet == null)
            {
                return NotFound();
            }

            return boxMachineProductionSheet;
        }

        [HttpPost]
        public async Task<ActionResult<BoxMachineProductionSheet>> CreateBoxMachineProductionSheet(BoxMachineProductionSheet boxMachineProductionSheet)
        {
            boxMachineProductionSheet.ProducedDate = DateTime.Now;
            _boxMachineInventaryContext.BoxMachineProductionSheet.Add(boxMachineProductionSheet);

            try
            {
                await _boxMachineInventaryContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

            return CreatedAtAction(nameof(GetBoxMachineProductionSheet), new { id = boxMachineProductionSheet.Id }, boxMachineProductionSheet);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBoxMachineProductionSheet(int id)
        {
            if (_boxMachineInventaryContext.BoxMachineProductionSheet == null)
            {
                return NotFound();
            }

            var boxMachineProductionSheet = await _boxMachineInventaryContext.BoxMachineProductionSheet.FindAsync(id);

            if (boxMachineProductionSheet == null)
            {
                return NotFound();
            }

            _boxMachineInventaryContext.BoxMachineProductionSheet.Remove(boxMachineProductionSheet);
            await _boxMachineInventaryContext.SaveChangesAsync();

            return Ok();
        }
    }
}
