using Institute_of_Fine_Arts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Institute_of_Fine_Arts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExhibitionController : ControllerBase
    {
        private readonly UserDbContext _dbContext;

        public ExhibitionController(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("AddExhibition")]
        public async Task<IActionResult> AddExhibition([FromForm] ExhibitionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data provided" });
            }

            string exhibitionPicturePath = null;

            // Handle image upload if provided
            if (model.ExhibitionPicture != null)
            {
                try
                {
                    // Define the upload directory
                    string uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "UserImages");
                    Directory.CreateDirectory(uploadDir); // Ensure the directory exists

                    // Generate a unique file name
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ExhibitionPicture.FileName);
                    string filePath = Path.Combine(uploadDir, fileName);

                    // Save the file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.ExhibitionPicture.CopyToAsync(stream);
                    }

                    // Save the relative path for storing in the database
                    exhibitionPicturePath = Path.Combine("UserImages/", fileName);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = "File upload failed", error = ex.Message });
                }
            }

            // Create a new Exhibition entity
            var exhibition = new Exhibitions
            {
                ExhibitionsName = model.ExhibitionsName,
                Description = model.Description,
                Location = model.Location,
                EnforceDeadline = model.EnforceDeadline,
                ExhibitionPicture = exhibitionPicturePath,
                StartDate = model.StartDate,
                EndDate = model.EndDate
            };

            // Save the exhibition to the database
            await _dbContext.exhibitions.AddAsync(exhibition);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Exhibition created successfully", exhibition });
        }





        // Read All
        [HttpGet("GetAllExhibitions")]
        public async Task<IActionResult> GetAllExhibitions()
        {
            var exhibitions = await _dbContext.exhibitions
                .Where(a => a.EnforceDeadline == true)
                .Select(exhibition => new
                {
                    exhibition.Id,
                    exhibition.ExhibitionsName,
                    exhibition.Description,
                    exhibition.Location,
                    exhibition.EnforceDeadline,
                    exhibition.ExhibitionPicture,
                    exhibition.StartDate,
                    exhibition.EndDate
                })
                .ToListAsync();

            return Ok(exhibitions);
        }


        // Read by Id
        [HttpGet("GetExhibition/{id}")]
        public async Task<IActionResult> GetExhibition(int id)
        {
            var exhibition = await _dbContext.exhibitions.FindAsync(id);
            if (exhibition == null)
            {
                return NotFound(new { message = "Exhibition not found" });
            }
            return Ok(exhibition);
        }

        // Update
        [HttpPut("UpdateExhibition/{id}")]
        public async Task<IActionResult> UpdateExhibition(int id, [FromForm] UpdateExhibitionModel model)
        {
           

            var existingExhibition = await _dbContext.exhibitions.FindAsync(id);
            if (existingExhibition == null)
            {
                return NotFound(new { message = "Exhibition not found" });
            }

            existingExhibition.ExhibitionsName = model.ExhibitionsName;
            existingExhibition.Description = model.Description;
            existingExhibition.Location = model.Location;
            existingExhibition.StartDate = model.StartDate;
            existingExhibition.EndDate = model.EndDate;
            existingExhibition.EnforceDeadline = model.EnforceDeadline;

            _dbContext.exhibitions.Update(existingExhibition);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Exhibition updated successfully", existingExhibition });
        }

        // Delete
        [HttpDelete("DeleteExhibition/{id}")]
        public async Task<IActionResult> DeleteExhibition(int id)
        {
            var exhibition = await _dbContext.exhibitions.FindAsync(id);
            if (exhibition == null)
            {
                return NotFound(new { message = "Exhibition not found" });
            }

            _dbContext.exhibitions.Remove(exhibition);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Exhibition deleted successfully" });
        }
    }
}
