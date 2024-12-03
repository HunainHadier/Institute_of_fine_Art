using System.ComponentModel.DataAnnotations;

namespace Institute_of_Fine_Arts.Models
{
    public class CourseViewModel
    {
        [Required]
        public string? CourseName { get; set; }

        [Required]
        public string? CourseDiscription { get; set; }

        [Required]
        public string? Requierments { get; set; }

       

        public IFormFile? CoursePicture { get; set; }

        [Required]
        public string? StartDate { get; set; }

        [Required]
        public string? EndDate { get; set; }
    }
}
