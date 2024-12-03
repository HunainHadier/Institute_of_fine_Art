using Institute_of_Fine_Arts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Institute_of_Fine_Arts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetitionRegisterController : ControllerBase
    {
        private readonly UserDbContext _dbContext;

        public CompetitionRegisterController(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("AddComStudent")]
        public async Task<IActionResult> AddComStudent([FromForm] ComRegView model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { errors = new { message = "Invalid data provided" } });
            }

            // Handle image upload if provided
            string imagePath = null;

            if (model.PaintingPicture != null)
            {
                // Define custom upload directory in project root
                string uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "UserImages");
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                // Generate a unique file name
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.PaintingPicture.FileName);
                imagePath = Path.Combine("UserImages/", fileName); // relative path
                string fullPath = Path.Combine(uploadDir, fileName); // full path

                // Save the file
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await model.PaintingPicture.CopyToAsync(fileStream);
                }
            }

            // Map ViewModel to Entity Model
            var competitionEntry = new CompetitionRegister
            {
                StudentName = model.StudentName,
                CompetitionName = model.CompetitionName,
                SubmissionDate = model.SubmissionDate,
                Title = model.Title,
                Description = model.Description,
                PaintingPicture = imagePath // Save the image path
            };

            // Add the award to the database
            await _dbContext.competitionstudent.AddAsync(competitionEntry);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Registration added successfully", competitionEntry });
        }

        [HttpGet("GetallRegStudent")]
        public async Task<IActionResult> GetallRegStudent()
        {
            if (_dbContext.competitionstudent == null)
            {
                return NotFound("No records found in the database.");
            }

            var Regstudent = await _dbContext.competitionstudent
                .Select(student => new CompetitionRegister
                {
                   
                    StudentName = student.StudentName,
                    CompetitionName = student.CompetitionName,
                    SubmissionDate = student.SubmissionDate,
                    Title = student.Title,
                    Description = student.Description,
                    PaintingPicture = student.PaintingPicture
                })
                .ToListAsync();

            return Ok(Regstudent);
        }




    }
}
