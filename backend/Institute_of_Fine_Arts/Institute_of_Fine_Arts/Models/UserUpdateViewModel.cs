using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
namespace Institute_of_Fine_Arts.Models
{
    public class UserUpdateViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public IFormFile? ProfilePicture { get; set; }
    }
}
