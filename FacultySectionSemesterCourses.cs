using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Flex.Models
{
    public class FacultySectionSemesterCourses
    {
        [System.ComponentModel.DataAnnotations.Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? SemesterCourseId { get; set; }

        [ForeignKey("SemesterCourseId")]

        public SemesterCourses SemCourses { get; set; }

        [MaxLength(450)]
        [DisplayName("Instructor Name")]
        public string? loginID { get; set; }

        public Userprofile Instructor { get; set; }

        [DisplayName("Section Name")]
        public int? SectionId { get; set; }

        [ForeignKey("SectionId")]
        public SectionSemesterCourses? SectionSemesterCourse { get; set; }

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
