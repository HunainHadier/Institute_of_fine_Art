using Microsoft.EntityFrameworkCore;

namespace Institute_of_Fine_Arts.Models
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

        public DbSet<User> users { get; set; }

        public DbSet<CompetitionModels> Competitions{ get; set; }

        public DbSet<Exhibitions> exhibitions { get; set; }

        public DbSet<Award> Awards { get; set; }

        public DbSet<Marking> Markings { get; set; }

        public DbSet<CompetitionRegister> competitionstudent { get; set; }

        public DbSet<ExhibitionRegistration> exhibitionRegistrations { get; set; }

        public DbSet<ContactUs> contacts{ get; set; }

        public DbSet<PaintingSubmission> paintingsubmit { get; set; }

        public DbSet<AllCourses> allCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            base.OnModelCreating(model);
            var adminpassword = BCrypt.Net.BCrypt.HashPassword("SuperAdmin123!");

            model.Entity<User>().HasData(new User
            {
                Id = Guid.NewGuid(),
                UserName = "SuperAdmin",
                FatherName = "Mazhar",
                FirstName = "hunain",
                LastName = "haider",
                Email = "superadmin@gmail",
                Password = adminpassword,
                PhoneNumber = "03182984945",
                Address = "Malir Karachi",
                Country = "Pakistan",
                PostalCode = "12345",
                Region = "Islam",
                ProfilePicture = "UserImage/149071.png",
                Role = "Admin",
                Course = "Admin"
               

            }


                );
            


        }







    }
}
