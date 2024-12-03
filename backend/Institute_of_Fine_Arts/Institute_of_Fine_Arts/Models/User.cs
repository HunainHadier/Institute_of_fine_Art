namespace Institute_of_Fine_Arts.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string UserName { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string FatherName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string? Gender { get; set;}


        public string? Password { get; set; } 

        public string? PhoneNumber { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? Region { get; set; }

        public string? PostalCode { get; set; }

        public string? Country { get; set; }

        public string? Course { get; set; }


        public string? ProfilePicture { get; set; }

        public string? Role { get; set; } = "user";

        public bool? IsActive { get; set; } = true;
        
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get;set; } = DateTime.Now;


    }
}
