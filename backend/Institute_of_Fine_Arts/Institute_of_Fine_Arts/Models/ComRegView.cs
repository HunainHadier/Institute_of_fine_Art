using Institute_of_Fine_Arts.Validations;
using System.ComponentModel.DataAnnotations;

namespace Institute_of_Fine_Arts.Models
{
    public class ComRegView
    {


        [Required]
        public string? StudentName { get; set; }


        [Required]
        public string? CompetitionName { get; set; }

        [Required]
        public string? SubmissionDate { get; set; }

        [Required]
        public string? Title { get; set; }


        public string? Description { get; set; }


        public IFormFile? PaintingPicture { get; set; }
    }
}
