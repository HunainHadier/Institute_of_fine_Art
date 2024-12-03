using Institute_of_Fine_Arts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Institute_of_Fine_Arts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly UserDbContext _dbContext;

        public ContactController(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        // Create
        [HttpPost("SendMass")]
        public async Task<IActionResult> SendMass([FromForm] ContactUs model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { errors = new { message = "Invalid data provided" } });
            }

            await _dbContext.contacts.AddAsync(model);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Your massage is successfully sended", model });
        }


        // Read All
        [HttpGet("ReadMass")]
        public async Task<IActionResult> ReadMass()
        {
            var Contact = await _dbContext.contacts.ToListAsync();
            return Ok(Contact);
        }


        // Delete
        [HttpDelete("DeleteMassage/{id}")]
        public async Task<IActionResult> DeleteMassage(int id)
        {
            var contact = await _dbContext.contacts.FindAsync(id);
            if (contact == null)
            {
                return NotFound(new { message = "Massage not found" });
            }

            _dbContext.contacts.Remove(contact);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Massage deleted successfully" });
        }


    }
}
