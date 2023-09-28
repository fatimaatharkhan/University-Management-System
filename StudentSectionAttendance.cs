using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flex.Models
{
    public class StudentSectionAttendance
    {
        [System.ComponentModel.DataAnnotations.Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int StudSecSemCourseId { get; set;}

        [ForeignKey("StudSecSemCourseId")]
        public StudentSectionSemesterCourses studentSecSemCourses { get; set; }

        public int SectionAttendanceId { get; set; }

        [ForeignKey("SectionAttendanceId")]
        public SectionAttendance sectionAttendance { get; set; }

        [DisplayName("Attendance")]
        public bool? present { get; set; }
    }
}
