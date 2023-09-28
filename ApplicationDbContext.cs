using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Flex.Models;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.AspNetCore.Identity;

namespace Flex.Data
{
    public class ApplicationDbContext : AuditableIdentityContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<Flex.Models.Userprofile>? Userprofile { get; set; }
        public DbSet<Flex.Models.Semester>? Semester { get; set; }
        public DbSet<Courses> Course { get; set; }

        public DbSet<Flex.Models.SemesterCourses>? SemesterCourses { get; set; }

        //public DbSet<Flex.Models.StudentSectionSemesterCourses>? StudentSemesterCourses { get; set; }

        public DbSet<Flex.Models.SectionSemesterCourses>? SectionSemesterCourses { get; set; }
        public DbSet<Flex.Models.StudentSectionSemesterCourses>? StudentSectionSemesterCourses { get; set; }

        public DbSet<Flex.Models.FacultySectionSemesterCourses>? FacultySectionSemesterCourses { get; set; }
        public DbSet<Flex.Models.SectionAttendance>? SectionAttendance { get; set; }

        public DbSet<Flex.Models.StudentSectionAttendance>? StudentSectionAttendance { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Userprofile>().HasIndex(x => x.loginID).IsUnique();  
            builder.Entity<StudentSectionSemesterCourses>().HasOne(x => x.Student).WithMany(x => x.StudentSemesterCourses).HasForeignKey(x => x.loginID).HasPrincipalKey(x => x.loginID);
            builder.Entity<FacultySectionSemesterCourses>().HasOne(x => x.Instructor).WithMany(x => x.FacultySectionSemesterCourses).HasForeignKey(x => x.loginID).HasPrincipalKey(x => x.loginID);
            InitData(builder);
        }

        private void InitData(ModelBuilder builder)
        {
            var hasher = new PasswordHasher<IdentityCustomFields>();
            var OfficerId = Guid.NewGuid().ToString();
            var FacultyId = Guid.NewGuid().ToString();
            var FacultyId1 = Guid.NewGuid().ToString();
            var FacultyId2 = Guid.NewGuid().ToString();
            var StudentId = Guid.NewGuid().ToString();
            var StudentId1 = Guid.NewGuid().ToString();
            var StudentId2 = Guid.NewGuid().ToString();


            builder.Entity<IdentityCustomFields>().HasData(
                new IdentityCustomFields
                {
                    Id = OfficerId,
                    Email = "FacultyAdmin@gmail.com",
                    UserName = "FacultyAdmin@gmail.com",
                    EmailConfirmed = true,
                    NormalizedEmail = "FacultyAdmin@gmail.com",
                    NormalizedUserName = "FacultyAdmin@gmail.com",
                    PasswordHash = hasher.HashPassword(null, "Test123!"),
                    UserType = 0
                },
                new IdentityCustomFields
                {
                    Id = FacultyId,
                    Email = "NoorulAin@gmail.com",
                    UserName = "NoorulAin@gmail.com",
                    EmailConfirmed = true,
                    NormalizedEmail = "NoorulAin@gmail.com",
                    NormalizedUserName = "NoorulAin@gmail.com",
                    PasswordHash = hasher.HashPassword(null, "Test123!"),
                    UserType = 1
                },
                new IdentityCustomFields
                {
                    Id = FacultyId1,
                    Email = "Nirmal@gmail.com",
                    UserName = "Nirmal@gmail.com",
                    EmailConfirmed = true,
                    NormalizedEmail = "Nirmal@gmail.com",
                    NormalizedUserName = "Nirmal@gmail.com",
                    PasswordHash = hasher.HashPassword(null, "Test123!"),
                    UserType = 1
                },
                new IdentityCustomFields
                {
                    Id = FacultyId2,
                    Email = "Owais@gmail.com",
                    UserName = "Owais@gmail.com",
                    EmailConfirmed = true,
                    NormalizedEmail = "Owais@gmail.com",
                    NormalizedUserName = "Owais@gmail.com",
                    PasswordHash = hasher.HashPassword(null, "Test123!"),
                    UserType = 1
                },
                new IdentityCustomFields
                {
                    Id = StudentId,
                    Email = "Fatima@gmail.com",
                    UserName = "Fatima@gmail.com",
                    EmailConfirmed = true,
                    NormalizedEmail = "Fatima@gmail.com",
                    NormalizedUserName = "Fatima@gmail.com",
                    PasswordHash = hasher.HashPassword(null, "Test123!"),
                    UserType = 2
                },
                new IdentityCustomFields
                {
                    Id = StudentId1,
                    Email = "Amna@gmail.com",
                    UserName = "Amna@gmail.com",
                    EmailConfirmed = true,
                    NormalizedEmail = "Amna@gmail.com",
                    NormalizedUserName = "Amna@gmail.com",
                    PasswordHash = hasher.HashPassword(null, "Test123!"),
                    UserType = 2
                },
                new IdentityCustomFields
                {
                    Id = StudentId2,
                    Email = "Salman@gmail.com",
                    UserName = "Salman@gmail.com",
                    EmailConfirmed = true,
                    NormalizedEmail = "Salman@gmail.com",
                    NormalizedUserName = "Salman@gmail.com",
                    PasswordHash = hasher.HashPassword(null, "Test123!"),
                    UserType = 2
                }
            );

            builder.Entity<Userprofile>().HasData(
                new Userprofile { Id = -7, FirstName = "Amir", LastName = "Khan", loginID = OfficerId, cnic = "111-111111-1", city = "IBD", country = "PK", phoneNumber = "0512260915", address = "G-11/4" },
                new Userprofile { Id = -6, FirstName = "Noorul", LastName = "Ain", loginID = FacultyId, cnic = "111-111111-1", city = "IBD", country = "PK", phoneNumber = "0512260915", address = "G-11/4" },
                new Userprofile { Id = -5, FirstName = "Nirmal", LastName = "Chaudhary", loginID = FacultyId1, cnic = "111-111111-1", city = "IBD", country = "PK", phoneNumber = "0512260915", address = "G-11/4" },
                new Userprofile { Id = -4, FirstName = "Owais", LastName = "Idrees", loginID = FacultyId2, cnic = "111-111111-1", city = "IBD", country = "PK", phoneNumber = "0512260915", address = "G-11/4" },
                new Userprofile { Id = -3, FirstName = "Fatima", LastName = "Athar Khan", loginID = StudentId, s_rollNumber = "21i0385", cnic = "111-111111-1", city = "IBD", country = "PK", phoneNumber = "0512260915", address = "G-11/4" },
                new Userprofile { Id = -2, FirstName = "Amna", LastName = "Usman", loginID = StudentId1, s_rollNumber = "21i2555", cnic = "111-111111-1", city = "IBD", country = "PK", phoneNumber = "0512260915", address = "G-11/4" },
                new Userprofile { Id = -1, FirstName = "Salman", LastName = "Jan", loginID = StudentId2, s_rollNumber = "21i2574", cnic = "111-111111-1", city = "IBD", country = "PK", phoneNumber = "0512260915", address = "G-11/4" }
                );


            builder.Entity<Courses>().HasData(
                new Courses { courseID = -10, courseCode = "CS001", courseDescription = "Programming Fundamental", courseName = "Programming Fundamental", creditHours = 3 },
                new Courses { courseID = -9, courseCode = "CS002", courseDescription = "Object Oriented Programming", courseName = "Object Oriented Programming", creditHours = 3 },
                new Courses { courseID = -8, courseCode = "CS003", courseDescription = "Operating System", courseName = "Operating System", creditHours = 3 },
                new Courses { courseID = -7, courseCode = "CS004", courseDescription = "Relational Database Systems", courseName = "Relational Database Systems", creditHours = 3 },
                new Courses { courseID = -6, courseCode = "CS005", courseDescription = "Design and Algorithm", courseName = "Design and Algorithm", creditHours = 3 },
                new Courses { courseID = -5, courseCode = "CS006", courseDescription = "Compiler Construction", courseName = "Compiler Construction", creditHours = 3 },
                new Courses { courseID = -4, courseCode = "CS007", courseDescription = "Computer Architecture", courseName = "Computer Architecture", creditHours = 3 }
                );

            builder.Entity<Semester>().HasData(

                new Semester
                {
                    Id = -10,
                    Description = "Fall Semester 2023",
                    Name = "Fall Semester 2023",
                    startDate = DateTime.Now.AddMonths(-5),
                    endDate = DateTime.Now,
                }
           );

            builder.Entity<SemesterCourses>().HasData(
                new SemesterCourses { Id = -10, SemId = -10, SCourseId = -10 },
                        new SemesterCourses { Id = -9, SemId = -10, SCourseId = -9 },
                        new SemesterCourses { Id = -8, SemId = -10, SCourseId = -8 },
                        new SemesterCourses { Id = -7, SemId = -10, SCourseId = -7 },
                        new SemesterCourses { Id = -6, SemId = -10, SCourseId = -6 },
                        new SemesterCourses { Id = -5, SemId = -10, SCourseId = -5 },
                        new SemesterCourses { Id = -4, SemId = -10, SCourseId = -4 }

                );

            builder.Entity<StudentSectionSemesterCourses>().HasData(
                new StudentSectionSemesterCourses { Id = -15, isApproved = false, loginID = StudentId, SemesterCourseId = -9 },
                new StudentSectionSemesterCourses { Id = -14, isApproved = false, loginID = StudentId, SemesterCourseId = -8 },
                new StudentSectionSemesterCourses { Id = -13, isApproved = false, loginID = StudentId, SemesterCourseId = -7 },
                new StudentSectionSemesterCourses { Id = -12, isApproved = false, loginID = StudentId, SemesterCourseId = -6 },
                new StudentSectionSemesterCourses { Id = -11, isApproved = false, loginID = StudentId, SemesterCourseId = -5 },
                new StudentSectionSemesterCourses { Id = -10, isApproved = false, loginID = StudentId, SemesterCourseId = -4 },
                new StudentSectionSemesterCourses { Id = -8, isApproved = false, loginID = StudentId1, SemesterCourseId = -9 },
                new StudentSectionSemesterCourses { Id = -7, isApproved = false, loginID = StudentId1, SemesterCourseId = -8 },
                new StudentSectionSemesterCourses { Id = -6, isApproved = false, loginID = StudentId1, SemesterCourseId = -7 },
                new StudentSectionSemesterCourses { Id = -5, isApproved = false, loginID = StudentId1, SemesterCourseId = -6 },
                new StudentSectionSemesterCourses { Id = -4, isApproved = false, loginID = StudentId1, SemesterCourseId = -5 },
                new StudentSectionSemesterCourses { Id = -3, isApproved = false, loginID = StudentId1, SemesterCourseId = -4 }
                // new StudentSectionSemesterCourses { Id = -8, isApproved = false, loginID = StudentId1, SemesterCourseId = -9 },
                //new StudentSectionSemesterCourses { Id = -7, isApproved = false, loginID = StudentId1, SemesterCourseId = -8 },
                //new StudentSectionSemesterCourses { Id = -6, isApproved = false, loginID = StudentId1, SemesterCourseId = -7 },
                //new StudentSectionSemesterCourses { Id = -5, isApproved = false, loginID = StudentId1, SemesterCourseId = -6 },
                //new StudentSectionSemesterCourses { Id = -4, isApproved = false, loginID = StudentId1, SemesterCourseId = -5 },
                //new StudentSectionSemesterCourses { Id = -3, isApproved = false, loginID = StudentId1, SemesterCourseId = -4 },
                //new StudentSectionSemesterCourses { Id = -2, isApproved = false, loginID = StudentId1, SemesterCourseId = -3 }

                );

            builder.Entity<FacultySectionSemesterCourses>().HasData(
                new FacultySectionSemesterCourses { Id = -12, loginID = FacultyId, SemesterCourseId = -9, assignments = 10, quizzes = 10, midTerms = 30, finals = 50 },
                new FacultySectionSemesterCourses { Id = -11, loginID = FacultyId, SemesterCourseId = -8, assignments = 10, quizzes = 10, midTerms = 30, finals = 50 },
                new FacultySectionSemesterCourses { Id = -10, loginID = FacultyId, SemesterCourseId = -7, assignments = 10, quizzes = 10, midTerms = 30, finals = 50 },
                new FacultySectionSemesterCourses { Id = -9, loginID = FacultyId1, SemesterCourseId = -10, assignments = 10, quizzes = 10, midTerms = 30, finals = 50 },
                new FacultySectionSemesterCourses { Id = -8, loginID = FacultyId1, SemesterCourseId = -8, assignments = 10, quizzes = 10, midTerms = 30, finals = 50 },
                new FacultySectionSemesterCourses { Id = -7, loginID = FacultyId1, SemesterCourseId = -7, assignments = 10, quizzes = 10, midTerms = 30, finals = 50 },
                new FacultySectionSemesterCourses { Id = -6, loginID = FacultyId1, SemesterCourseId = -6, assignments = 10, quizzes = 10, midTerms = 30, finals = 50 },
                new FacultySectionSemesterCourses { Id = -5, loginID = FacultyId2, SemesterCourseId = -4, assignments = 10, quizzes = 10, midTerms = 30, finals = 50 },
                new FacultySectionSemesterCourses { Id = -4, loginID = FacultyId2, SemesterCourseId = -5, assignments = 10, quizzes = 10, midTerms = 30, finals = 50 },
                new FacultySectionSemesterCourses { Id = -3, loginID = FacultyId2, SemesterCourseId = -6, assignments = 10, quizzes = 10, midTerms = 30, finals = 50 }
                );





            //builder.Entity<Courses>().HasData(
            //    new Courses { courseCode = "CS001", courseDescription = "Programming Fundamendatal", courseName = "Programming Fundamendatal", creditHours = 3 }
            //    new Courses { courseCode = "CS002", courseDescription = "Object Oriented Programming", courseName = "Programming Fundamendatal", creditHours = 3 }
            //    new Courses { courseCode = "CS003", courseDescription = "Operating System", courseName = "Programming Fundamendatal", creditHours = 3 }
            //    new Courses { courseCode = "CS004", courseDescription = "Relational Database Systems", courseName = "Programming Fundamendatal", creditHours = 3 }
            //    new Courses { courseCode = "CS005", courseDescription = "Design and Algorithm", courseName = "Programming Fundamendatal", creditHours = 3 }
            //    new Courses { courseCode = "CS006", courseDescription = "Compiler Construction", courseName = "Programming Fundamendatal", creditHours = 3 }
            //    new Courses { courseCode = "CS007", courseDescription = "Computer Architecture", courseName = "Programming Fundamendatal", creditHours = 3 }

            //    );
        }
    }
}