using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flex.Models
{
    public class SectionSemesterCourses
    {
        [System.ComponentModel.DataAnnotations.Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? SemesterCourseId { get; set; }

        [ForeignKey("SemesterCourseId")]

        public SemesterCourses SemCourses { get; set; }

        [DisplayName("Section Name")]
        public string Name { get; set; }

        public string Description { get; set; }

        public List<StudentSectionSemesterCourses> StudentSectionSemesterCourses { get; set; } 
        
        public FacultySectionSemesterCourses? FacultSection { get; set; }

        public List<SectionAttendance> sectionAttendances { get; set; }
    }
}
