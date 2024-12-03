using Institute_of_Fine_Arts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Institute_of_Fine_Arts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {

        private readonly UserDbContext _dbContext;

        public CourseController(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("AddCourses")]
        public async Task<IActionResult> AddCourses([FromForm] CourseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data provided" });
            }

            string CoursePicture = null;

            // Handle image upload if provided
            if (model.CoursePicture != null)
            {
                try
                {
                    // Define the upload directory
                    string uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "UserImages");
                    Directory.CreateDirectory(uploadDir); // Ensure the directory exists

                    // Generate a unique file name
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.CoursePicture.FileName);
                    string filePath = Path.Combine(uploadDir, fileName);

                    // Save the file
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await model.CoursePicture.CopyToAsync(stream);
                    }

                    // Save the relative path for storing in the database
                    CoursePicture = Path.Combine("UserImages/", fileName);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = "File upload failed", error = ex.Message });
                }
            }

            // Create a new Exhibition entity
            var Courses = new AllCourses
            {
              CourseName = model.CourseName,
              CourseDiscription = model.CourseDiscription,
              Requierments = model.Requierments,
              CoursePicture = CoursePicture,
              EndDate = model.EndDate,
              StartDate = model.StartDate

            };

            // Save the exhibition to the database
            await _dbContext.allCourses.AddAsync(Courses);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Coures created successfully", Courses });
        }



        // Read All
        [HttpGet("GetCourses")]
        public async Task<IActionResult> GetCourses()
        {
            var courses = await _dbContext.allCourses.ToListAsync();
            return Ok(courses);
        }

        // Read by Id
        [HttpGet("GetCourses/{id}")]
        public async Task<IActionResult> GetCourses(int id)
        {
            var courses = await _dbContext.allCourses.FindAsync(id);
            if (courses == null)
            {
                return NotFound(new { message = "Competition not found" });
            }
            return Ok(courses);
        }

        // Update
        [HttpPut("UpdateCourses/{id}")]
        public async Task<IActionResult> UpdateCompetition(int id, [FromForm] UpdateCoursesModel model)
        {


            var existingCourses= await _dbContext.allCourses.FindAsync(id);
            if (existingCourses == null)
            {
                return NotFound(new { message = "Competition not found" });
            }
            existingCourses.CourseName = model.CourseName;
            existingCourses.CourseDiscription = model.CourseDiscription;
            existingCourses.Requierments = model.Requierments;
            existingCourses.EndDate = model.EndDate;
            existingCourses.StartDate = model.StartDate;



            _dbContext.allCourses.Update(existingCourses);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Courses updated successfully", existingCourses });
        }

        // Delete
        [HttpDelete("DeleteCourses/{id}")]
        public async Task<IActionResult> DeleteCompetition(int id)
        {
            var courses = await _dbContext.allCourses.FindAsync(id);
            if (courses == null)
            {
                return NotFound(new { message = "Courses not found" });
            }

            _dbContext.allCourses.Remove(courses);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Course deleted successfully" });
        }

        [HttpGet("count-Courses")]
        public IActionResult CountCOurses()
        {
            var CoursesCount = _dbContext.allCourses.Count();
            return Ok(CoursesCount);
        }




    }
}
