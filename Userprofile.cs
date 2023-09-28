using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flex.Models
{
    public class Userprofile
    {

        [System.ComponentModel.DataAnnotations.Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //[Required]
        [MaxLength(50)]
        [DisplayName("First Name")]
        public string? FirstName { get; set; }

        [MaxLength(50)]
        [DisplayName("Last Name")]
        public string? LastName { get; set; }

        //[Required]
        //[DisplayName("Course Description")]
        [MaxLength(450)]
        public string? loginID { get; set; }

        [MaxLength(50)]
        [DisplayName("Phone Number")]
        public string? phoneNumber { get; set; }

        [MaxLength(15)]
        [DisplayName("Roll Number")]
        public string? s_rollNumber { get; set; }

        [MaxLength(15)]
        [DisplayName("Section")]
        public string? s_section { get; set; }

        [MaxLength(50)]
        [DisplayName("Degree")]
        public string? s_degree { get; set; }

        [MaxLength(50)]
        [DisplayName("Gender")]
        public string? gender { get; set;}

        [MaxLength(50)]
        [DisplayName("Batch")]
        public string? s_batch { get; set; }

        //[Required]
        [DisplayName("Date of Birth")]
        public DateTime? DOB { get; set; }

        [MaxLength(50)]
        [DisplayName("City")]
        public string? city { get; set; }

        [MaxLength(50)]
        [DisplayName("Country")]
        public string? country { get; set; }

        [MaxLength(50)]
        [DisplayName("CNIC")]
        public string? cnic { get; set; }

        [MaxLength(200)]
        [DisplayName("Address")]
        public string? address { get; set; }

        public List<StudentSectionSemesterCourses>? StudentSemesterCourses { get; set; }
        
        public List<FacultySectionSemesterCourses>? FacultySectionSemesterCourses { get; set; }

    }
}
