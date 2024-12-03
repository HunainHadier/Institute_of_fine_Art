using System.ComponentModel.DataAnnotations;

namespace Institute_of_Fine_Arts.Models
{
    public class ExhibitionRegistration
    {
        [Key]
        public int? Id { get; set; }

        [Required]
        public string? FullName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        public string? ContactNumber { get; set; }

        [Required]
        public string? ClassorBatch {  get; set; }

        [Required]
        public string? ArtWorkTite {  get; set; }

        [Required]
        public string? PaintingCategory { get; set; }

        [Required]
        public string? CategoryUsed { get; set; }


        [Required]
        public string? Price { get; set; }

        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    }
}
