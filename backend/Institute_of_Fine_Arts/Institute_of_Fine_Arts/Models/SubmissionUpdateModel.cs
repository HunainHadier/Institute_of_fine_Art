namespace Institute_of_Fine_Arts.Models
{
    public class SubmissionUpdateModel
    {

        public string? StudentName { get; set; }

        public string? PaintingTitle { get; set; }

        public string? PaintingDiscription { get; set; }

        public string? PaintingImage { get; set; }

        public string? Price { get; set; }

        public DateTime? SubmitDate { get; set; } = DateTime.Now;

    }
}
