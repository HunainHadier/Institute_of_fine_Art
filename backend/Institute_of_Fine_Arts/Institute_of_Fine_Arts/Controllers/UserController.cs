using Institute_of_Fine_Arts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Security.Claims;

namespace Institute_of_Fine_Arts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _dbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserController(UserDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _webHostEnvironment = webHostEnvironment;

        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { errors = new { message = "Invalid data provided" } });
            }

            if (await EmailExist(model.Email))
            {
                return BadRequest(new { errors = new { Email = new[] { "Email Already Exist" } } });
            }

            if (await UserNameExist(model.UserName))
            {
                return BadRequest(new { errors = new { UserName = new[] { "UserName Already Exist" } } });
            }
            // images upload
            string profilePicturePath = null;

            if (model.ProfilePicture != null)
            {
                // Define custom upload directory in project root
                string uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "UserImages");
                if (!Directory.Exists(uploadDir))
                {
                    Directory.CreateDirectory(uploadDir);
                }

                // Generate a unique file name
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.ProfilePicture.FileName);
                profilePicturePath = Path.Combine("UserImages/", fileName); // relative path
                string fullPath = Path.Combine(uploadDir, fileName); // full path

                // Save the file
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    await model.ProfilePicture.CopyToAsync(fileStream);
                }
            }

            // Create user object
            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = model.UserName,
                FirstName = model.FirstName,
                LastName = model.LastName,
                FatherName = model.FatherName,
                City = model.City,
                Country = model.Country,
                Course = model.Course,
                PostalCode = model.PostalCode,
                Email = model.Email,
                Gender = model.Gender,
                Password = HashPassword(model.Password),
                ProfilePicture = profilePicturePath, // Save the file path in the database
                Role = model.Role,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _dbContext.users.Add(user);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Registration Successful" });
        }

        private string HashPassword(string? password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private async Task<bool> UserNameExist(string username)
        {
            return await _dbContext.users.AnyAsync(backendname => backendname.UserName == username);
        }

        private async Task<bool> EmailExist(string email)
        {
            return await _dbContext.users.AnyAsync(backendeamil => backendeamil.Email == email);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { errors = new { message = "Invalid login data." } });
            }

            var user = await _dbContext.users.SingleOrDefaultAsync(udata => udata.UserName == model.UserName);
            if (user == null)
            {
                return BadRequest(new { errors = new { message = "Invalid UserName." } });
            }

            if (!VerifyPassword(model.Password, user.Password))
            {
                return BadRequest(new { errors = new { message = "Invalid Password." } });
            }

            return Ok(new { message = "Login Successfully", username = user.UserName, role = user.Role});
        }

        private bool VerifyPassword(string? frontpassword, string? backpassword)
        {
            return BCrypt.Net.BCrypt.Verify(frontpassword, backpassword);
        }

        [HttpGet("count-students")]
        public IActionResult CountStudents()
        {
            var studentCount = _dbContext.users.Count(u => u.Role == "Student");
            return Ok(studentCount);
        }

        [HttpGet("count-Staff")]
        public IActionResult CountStaff()
        {
            var staffCount = _dbContext.users.Count(b => b.Role == "Staff");
            return Ok(staffCount);
        }


        [HttpGet("GetAllStudent")]
        public async Task<IActionResult> GetAllStudent()
        {
            var AllStudent = await _dbContext.users.Where(all => all.Role == "Student").ToListAsync();

            if (AllStudent == null)
            {
                return NotFound(new { message = "Student not found" });
            }

            // Add the full image URL to the profilePicture property for each student
            var studentsWithProfilePics = AllStudent.Select(student => new
            {
                student.UserName,
                student.Email,
                student.PhoneNumber,
                student.Course,
                student.ProfilePicture
            }).ToList();

            return Ok(studentsWithProfilePics);
        }

        //get staff

        [HttpGet("GetAllStaff")]
        public async Task<IActionResult> GetAllStaff()
        {
            var AllStaff = await _dbContext.users.Where(all => all.Role == "Staff").ToListAsync();

            if (AllStaff == null)
            {
                return NotFound(new { message = "Staff not found" });
            }

            // Add the full image URL to the profilePicture property for each student
            var studentsWithProfilePics = AllStaff.Select(staff => new
            {
                staff.UserName,
                staff.Email,
                staff.PhoneNumber,
                staff.Course,
                ProfilePicture = $"{Request.Scheme}://{Request.Host}/api/User/images/{staff.ProfilePicture}" // Full URL for the image
            }).ToList();

            return Ok(studentsWithProfilePics);
        }


        [HttpDelete("delete/{Email}")]
        public async Task<IActionResult> DeleteUserByRole(string Email)
        {
            // Find users with the given role
            var users = await _dbContext.users.Where(u => u.Email == Email).ToListAsync();

            if (users.Count == 0)
            {
                return NotFound(new { message = $"No users found with the role '{Email}'." });
            }

            // Remove all users with the specified role
            _dbContext.users.RemoveRange(users);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = $"{Email} users deleted successfully." });
        }




    }


}
