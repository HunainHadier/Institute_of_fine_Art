using System.ComponentModel.DataAnnotations;

namespace Institute_of_Fine_Arts.Models
{
    public class CompetitionViewModel
    {
        [Required(ErrorMessage = "Competition Name is Required")]
        public string CompetitionName { get; set; } = string.Empty;

        public string? StartDate { get; set; }

        [Required(ErrorMessage = "End Date is Required")]
        public string? EndDate { get; set; }

        public IFormFile? CompetitonPicture { get; set; }

        [Required(ErrorMessage = "Submission Format is Required")]
        public string? SubmissionFormat { get; set; }

        [Required(ErrorMessage = "Description Requirement is Required")]
        public string? DescriptionRequirement { get; set; }

        public bool? EnforceDeadline { get; set; } = true;


    }
}
