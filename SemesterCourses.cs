using System.ComponentModel.DataAnnotations.Schema;

namespace Flex.Models
{
    public class SemesterCourses
    {
        [System.ComponentModel.DataAnnotations.Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int SemId { get; set; }

        [ForeignKey("SemId")]
        public Semester? semester { get; set; }

        public int SCourseId { get; set; }

        [ForeignKey("SCourseId")]
        public Courses? Course { get; set; }

        public List<SectionSemesterCourses> SectionSemesterCourses { get; set; }
    }
}
