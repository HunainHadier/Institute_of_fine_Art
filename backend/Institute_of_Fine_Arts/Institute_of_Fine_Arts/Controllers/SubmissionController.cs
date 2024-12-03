using Institute_of_Fine_Arts.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Institute_of_Fine_Arts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        private readonly UserDbContext _dbContext;

        public SubmissionController(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("AddPainting")]
        public async Task<IActionResult> AddPainting([FromForm] SubmissionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Invalid data provided" });
            }

            string PaintingsPicturePath = null;

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
                    PaintingsPicturePath = Path.Combine("UserImages/", fileName);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, new { message = "File upload failed", error = ex.Message });
                }
            }

            // Create a new Exhibition entity
            var submitpainting = new PaintingSubmission
            {
                             
                StudentName = model.StudentName,
                PaintingTitle = model.PaintingTitle,
                PaintingImage  = PaintingsPicturePath,
                PaintingDiscription = model.PaintingDiscription,
                Price = model.Price,



            };

            // Save the exhibition to the database
            await _dbContext.paintingsubmit.AddAsync(submitpainting);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Exhibition created successfully", submitpainting });
        }


        // Read All
        [HttpGet("GetAllpaintings")]
        public async Task<IActionResult> GetAllpaintings()
        {
            var submitpainting = await _dbContext.paintingsubmit.ToListAsync();
            return Ok(submitpainting);
        }

        // Read by Id
        [HttpGet("Getpainting/{id}")]
        public async Task<IActionResult> Getpainting(int id)
        {
            var Paint = await _dbContext.paintingsubmit.FindAsync(id);
            if (Paint == null)
            {
                return NotFound(new { message = "Painting is  not found" });
            }
            return Ok(Paint);
        }

        // Update
        [HttpPut("Updatepainting/{id}")]
        public async Task<IActionResult> Updatepainting(int id, [FromForm] SubmissionUpdateModel model)
        {


            var existingPainting = await _dbContext.paintingsubmit.FindAsync(id);
            if (existingPainting == null)
            {
                return NotFound(new { message = "Painting not found" });
            }

            existingPainting.StudentName = model.StudentName;
            existingPainting.PaintingDiscription = model.PaintingDiscription;
            existingPainting.PaintingTitle = model.PaintingTitle;
            existingPainting.Price = model.Price;
            existingPainting.PaintingTitle = model.PaintingImage;
        

            _dbContext.paintingsubmit.Update(existingPainting);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Submission updated successfully", existingPainting });
        }

        // Delete
        [HttpDelete("Deletepainting/{id}")]
        public async Task<IActionResult> Deletepainting(int id)
        {
            var Submitpainting = await _dbContext.paintingsubmit.FindAsync(id);
            if (Submitpainting == null)
            {
                return NotFound(new { message = "painting is not found" });
            }

            _dbContext.paintingsubmit.Remove(Submitpainting);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = "Painting deleted successfully" });
        }
    }








}

