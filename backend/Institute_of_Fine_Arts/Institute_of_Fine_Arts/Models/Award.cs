using System.ComponentModel.DataAnnotations;

namespace Institute_of_Fine_Arts.Models
{
    public class Award
    {
        [Key]
        public int? Id { get; set; }

        public string? StudentName { get; set; }

        public string? FatherName { get; set; }

        public string? PaintingTitle { get; set; }

        public string? PaintingImage { get; set; }

        public string? Remarks { get; set; }

        public string? Awards { get; set; }




    }
}
