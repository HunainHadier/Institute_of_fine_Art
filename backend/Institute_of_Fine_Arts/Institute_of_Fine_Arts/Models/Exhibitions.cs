using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Institute_of_Fine_Arts.Models
{
    public class Exhibitions
    {
        [Key]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Exhibition Name is required")]
        public string? ExhibitionsName { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string? Location { get; set; }

        public bool? EnforceDeadline { get; set; } = true;

        // This will store the file path of the uploaded image
        public string? ExhibitionPicture { get; set; }

        [Required(ErrorMessage = "Start Date is required")]
        public string? StartDate { get; set; }

        [Required(ErrorMessage = "End Date is required")]
        public string? EndDate { get; set; }

    
    }
}
