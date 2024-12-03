using System.ComponentModel.DataAnnotations;

namespace Institute_of_Fine_Arts.Models
{
    public class AllCourses
    {
        [Key]
        public int? Id { get; set; }

        public string? CourseName { get; set; }

        public string? CourseDiscription { get; set; }
        
        public string? Requierments { get; set; }

        public string? CoursePicture { get; set; }

        public string? StartDate { get; set; }

        public string? EndDate { get; set; }
    }
}
