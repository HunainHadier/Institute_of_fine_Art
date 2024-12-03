using System.ComponentModel.DataAnnotations;

namespace Institute_of_Fine_Arts.Models
{
    public class ContactUs
    {
        [Key]
        public int? Id { get; set; }

        public string? Name { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? Massage { get; set; }


    }
}
