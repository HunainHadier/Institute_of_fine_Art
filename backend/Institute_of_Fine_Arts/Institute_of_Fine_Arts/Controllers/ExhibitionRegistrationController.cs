using Institute_of_Fine_Arts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Institute_of_Fine_Arts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExhibitionRegistrationController : ControllerBase
    {
        private readonly UserDbContext _dbContext;

        public ExhibitionRegistrationController(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // Create
        [HttpPost("AddExhibitionStudent")]
        public async Task<IActionResult> AddExhibitionStudent([FromForm] ExhibitionRegistration model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { errors = new { message = "Invalid data provided" } });
            }

            await _dbContext.exhibitionRegistrations.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Your Registration is successfully Done", model });
        }


        // Read All
        [HttpGet("GetallRegisterStudent")]
        public async Task<IActionResult> GetallRegisterStudent()
        {
            var exhibition = await _dbContext.exhibitionRegistrations.ToListAsync();
            return Ok(exhibition);
        }







        // Delete
        [HttpDelete("DeleteStudentresgistration/{id}")]
        public async Task<IActionResult> DeleteStudentresgistration(int id)
        {
            var exhibition = await _dbContext.exhibitionRegistrations.FindAsync(id);
            if (exhibition == null)
            {
                return NotFound(new { message = "Registration not found" });
            }

            _dbContext.exhibitionRegistrations.Remove(exhibition);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Your Registration is successfully deleted" });
        }


    }

}
