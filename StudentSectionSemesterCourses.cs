using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flex.Models
{
    public class StudentSectionSemesterCourses
    {
        [System.ComponentModel.DataAnnotations.Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? SemesterCourseId { get; set;}

        [ForeignKey("SemesterCourseId")]

        public SemesterCourses SemCourses { get;set; }

        [MaxLength(450)]
        [DisplayName("Student Name")]
        public string? loginID { get; set; }

        public Userprofile Student { get; set; }

        [DisplayName("Status")]
        public Boolean? isApproved { get; set; }

        [DisplayName("Section Name")]
        public int? SectionId { get; set; }

        [ForeignKey("SectionId")]
        public SectionSemesterCourses SectionSemesterCourse { get; set; }

        public List<StudentSectionAttendance> StudentSectionAttendances { get; set; }

        [DisplayName("Assignments")]
        public int? assignments { get; set; }

        [DisplayName("Quizzes")]
        public int? quizzes { get; set; }

        [DisplayName("Mid Terms")]
        public int? midTerms { get; set; }

        [DisplayName("Final Exams")]
        public int? finals { get; set; }

    }
}
