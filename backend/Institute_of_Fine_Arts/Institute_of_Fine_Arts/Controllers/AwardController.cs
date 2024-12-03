using Institute_of_Fine_Arts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Institute_of_Fine_Arts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AwardController : ControllerBase
    {
        private readonly UserDbContext _dbContext;

        public AwardController(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("AddAward")]
        public async Task<IActionResult> AddAward([FromForm] AwardViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data provided" });
            }

            string AwardPicturePath = null;

            // Handle image upload if provided
            if (model.PaintingImage != null)
            {
                try
                {
                    // Define the upload directory
                    string uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "UserImages");
                    Directory.CreateDirectory(uploadDir); // Ensure the directory exists

                    // Generate a unique file name
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.PaintingImage.FileName);
                    string filePath = Path.Combine(uploadDir, fileName);

                    // Save the file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.PaintingImage.CopyToAsync(stream);
                    }

                    // Save the relative path for storing in the database
                    AwardPicturePath = Path.Combine("UserImages/", fileName);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = "File upload failed", error = ex.Message });
                }
            }

            // Create a new Exhibition entity
            var award = new Award
            {
               StudentName = model.StudentName,
               FatherName = model.FatherName,
               PaintingTitle = model.PaintingTitle,
               PaintingImage = AwardPicturePath,
               Remarks = model.Remarks,
               Awards = model.Awards,



            };

            // Save the exhibition to the database
            await _dbContext.Awards.AddAsync(award);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Exhibition created successfully", award });
        }


        // Read All
        [HttpGet("GetAllAwards")]
        public async Task<IActionResult> GetAllAwards()
        {
            var award = await _dbContext.Awards.ToListAsync();
            return Ok(award);
        }

        // Read by Id
        [HttpGet("GetAward/{id}")]
        public async Task<IActionResult> GetAward(int id)
        {
            var award = await _dbContext.Awards.FindAsync(id);
            if (award == null)
            {
                return NotFound(new { message = "Award not found" });
            }
            return Ok(award);
        }

        // Update
        [HttpPut("UpdateAward/{id}")]
        public async Task<IActionResult> UpdateAward(int id, [FromForm] UpdateAwardModel model)
        {
           

            var existingAward = await _dbContext.Awards.FindAsync(id);
            if (existingAward == null)
            {
                return NotFound(new { message = "Award not found" });
            }

            existingAward.StudentName = model.StudentName;
            existingAward.FatherName = model.FatherName;
            existingAward.PaintingTitle = model.PaintingTitle;
            existingAward.Remarks = model.Remarks;
            existingAward.Awards = model.Awards;

            _dbContext.Awards.Update(existingAward);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Award updated successfully", existingAward });
        }

        // Delete
        [HttpDelete("DeleteAward/{id}")]
        public async Task<IActionResult> DeleteAward(int id)
        {
            var award = await _dbContext.Awards.FindAsync(id);
            if (award == null)
            {
                return NotFound(new { message = "Awards not found" });
            }

            _dbContext.Awards.Remove(award);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Award deleted successfully" });
        }
    }






}
