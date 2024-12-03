using Institute_of_Fine_Arts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Institute_of_Fine_Arts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarkingController : ControllerBase
    {
        private readonly UserDbContext _dbContext;

        public MarkingController(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create
        [HttpPost("AddMarking")]
        public async Task<IActionResult> AddMarking([FromForm] Marking model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { errors = new { message = "Invalid data provided" } });
            }

            _dbContext.Markings.Add(model);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Marking created successfully", model });
        }

        // Read All
        [HttpGet("GetMarkings")]
        public async Task<IActionResult> GetMarkings()
        {
            var markings = await _dbContext.Markings.ToListAsync();
            return Ok(markings);
        }

        // Read by Id
        [HttpGet("GetMarking/{id}")]
        public async Task<IActionResult> GetMarking(int id)
        {
            var marking = await _dbContext.Markings.FindAsync(id);
            if (marking == null)
            {
                return NotFound(new { message = "Marking not found" });
            }
            return Ok(marking);
        }

        // Update
        [HttpPut("UpdateMarking/{id}")]
        public async Task<IActionResult> UpdateMarking(int id, [FromForm] Marking model)
        {
            if (id != model.Id || !ModelState.IsValid)
            {
                return BadRequest(new { errors = new { message = "Invalid data provided" } });
            }

            var existingMarking = await _dbContext.Markings.FindAsync(id);
            if (existingMarking == null)
            {
                return NotFound(new { message = "Marking not found" });
            }

            // Update the properties
            existingMarking.StaffName = model.StaffName;
            existingMarking.Marks = model.Marks;
            existingMarking.SubmissionDate = model.SubmissionDate;

            _dbContext.Markings.Update(existingMarking);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Marking updated successfully", existingMarking });
        }

        // Delete
        [HttpDelete("DeleteMarking/{id}")]
        public async Task<IActionResult> DeleteMarking(int id)
        {
            var marking = await _dbContext.Markings.FindAsync(id);
            if (marking == null)
            {
                return NotFound(new { message = "Marking not found" });
            }

            _dbContext.Markings.Remove(marking);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Marking deleted successfully" });
        }
    }
}
