using System.ComponentModel.DataAnnotations;

namespace Institute_of_Fine_Arts.Models
{
    public class UpdateExhibitionModel
    {
        [Required(ErrorMessage = "Competition Name is Required")]
        public string? ExhibitionsName { get; set; }

        [Required(ErrorMessage = "Competition Name is Required")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Competition Name is Required")]
        public string? Location { get; set; }

        public bool? EnforceDeadline { get; set; } = true;

        public string? StartDate { get; set; }

        public string? EndDate { get; set; }
    }
}
