using System.ComponentModel.DataAnnotations;

namespace Institute_of_Fine_Arts.Models
{
    public class UpdateAwardModel
    {
        [Required]
        public string? StudentName { get; set; }
        [Required]
        public string? FatherName { get; set; }
        [Required]
        public string? PaintingTitle { get; set; }

        public IFormFile? PaintingImage { get; set; }
        [Required]
        public string? Remarks { get; set; }
        [Required]
        public string? Awards { get; set; }


    }
}
