using System.ComponentModel.DataAnnotations;

namespace Institute_of_Fine_Arts.Models
{
    public class Marking
    {
        [Key]
        public int? Id { get; set; }

        public string? StaffName { get; set; }

        public string? Marks { get; set; }

        public DateTime? SubmissionDate { get; set; }

    }
}
