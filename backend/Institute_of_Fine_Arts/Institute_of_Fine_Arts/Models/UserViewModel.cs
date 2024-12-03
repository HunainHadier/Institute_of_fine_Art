using Institute_of_Fine_Arts.Validations;
using System.ComponentModel.DataAnnotations;

namespace Institute_of_Fine_Arts.Models
{
    public class UserViewModel
    {
        [Required(ErrorMessage ="User Name is Required")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "FirstName is Required")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last Name is Required")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Father Name is Required")]
        public string FatherName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;


        [Required(ErrorMessage = "Gender is Required")]
        public string? Gender { get; set; }


        [PassowrdValidator]
        public string? Password { get; set; }

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? Region { get; set; }

        public string? PostalCode { get; set; }

        public string? Country { get; set; }

        public string? Course { get; set; }

        public IFormFile? ProfilePicture { get; set; }

        [Required(ErrorMessage = "Role is Required")]
        public string? Role { get; set; } = "user";

        public bool? IsActive { get; set; } = true;

        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; } = DateTime.Now;

    }
}
