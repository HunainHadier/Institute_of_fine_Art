using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace Institute_of_Fine_Arts.Models
{
    public class PaintingSubmission
    {
        [Key]
        public int? Id { get; set; }

        public string? StudentName { get; set; }

        public string? PaintingTitle { get; set; }

        public string? PaintingDiscription { get; set; }

        public string? PaintingImage { get; set; }

        public string? Price { get; set; }

        public DateTime? SubmitDate { get; set; } = DateTime.Now;
 
    }
}
