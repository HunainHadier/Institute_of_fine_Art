using Institute_of_Fine_Arts.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Institute_of_Fine_Arts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitionController : ControllerBase
    {
        private readonly UserDbContext _dbContext;

        public CompetitionController(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("AddCompetition")]
        public async Task<IActionResult> AddCompetition([FromForm] CompetitionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data provided" });
            }

            string competitionPicturePath = null;

            // Handle image upload if provided
            if (model.CompetitonPicture != null)
            {
                try
                {
                    // Define the upload directory
                    string uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "UserImages");
                    Directory.CreateDirectory(uploadDir); // Ensure the directory exists

                    // Generate a unique file name
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.CompetitonPicture.FileName);
                    string filePath = Path.Combine(uploadDir, fileName);

                    // Save the file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.CompetitonPicture.CopyToAsync(stream);
                    }

                    // Save the relative path for storing in the database
                    competitionPicturePath = Path.Combine("UserImages/", fileName);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = "File upload failed", error = ex.Message });
                }
            }

            // Create a new Exhibition entity
            var competition = new CompetitionModels
            {
                CompetitionName = model.CompetitionName,
                DescriptionRequirement = model.DescriptionRequirement,
                CompetitonPicture = competitionPicturePath,
                SubmissionFormat = model.SubmissionFormat,  
                EndDate = model.EndDate,
                StartDate  = model.StartDate,
               EnforceDeadline = model.EnforceDeadline,

            };

            // Save the exhibition to the database
            await _dbContext.Competitions.AddAsync(competition);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Exhibition created successfully", competition });
        }



        // Read All
        [HttpGet("GetCompetitions")]
        public async Task<IActionResult> GetCompetitions()
        {
            var competitions = await _dbContext.Competitions.Where(a => a.EnforceDeadline == true).ToListAsync();
            return Ok(competitions);
        }

        // Read by Id
        [HttpGet("GetCompetition/{id}")]
        public async Task<IActionResult> GetCompetition(int id)
        {
            var competition = await _dbContext.Competitions.FindAsync(id);
            if (competition == null)
            {
                return NotFound(new { message = "Competition not found" });
            }
            return Ok(competition);
        }

        // Update
        [HttpPut("UpdateCompetition/{id}")]
        public async Task<IActionResult> UpdateCompetition(int id, [FromForm] UpdateCompetitionModel model)
        {
          

            var existingCompetition = await _dbContext.Competitions.FindAsync(id);
            if (existingCompetition == null)
            {
                return NotFound(new { message = "Competition not found" });
            }

            existingCompetition.CompetitionName = model.CompetitionName;
            existingCompetition.StartDate = model.StartDate;
            existingCompetition.EndDate = model.EndDate;
            existingCompetition.SubmissionFormat = model.SubmissionFormat;
            existingCompetition.DescriptionRequirement = model.DescriptionRequirement;
            existingCompetition.EnforceDeadline = model.EnforceDeadline;

            _dbContext.Competitions.Update(existingCompetition);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Competition updated successfully", existingCompetition });
        }

        // Delete
        [HttpDelete("DeleteCompetition/{id}")]
        public async Task<IActionResult> DeleteCompetition(int id)
        {
            var competition = await _dbContext.Competitions.FindAsync(id);
            if (competition == null)
            {
                return NotFound(new { message = "Competition not found" });
            }

            _dbContext.Competitions.Remove(competition);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Competition deleted successfully" });
        }

        [HttpGet("count-ompetition")]
        public IActionResult CountCompetition()
        {
            var competitonCount = _dbContext.Competitions.Count();
            return Ok(competitonCount);
        }


    }
}
