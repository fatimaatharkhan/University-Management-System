using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flex.Models
{
    public class SectionAttendance
    {
        [System.ComponentModel.DataAnnotations.Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DisplayName("Section Name")]
        public int SectionId { get; set;}

        [ForeignKey("SectionId")]
        public SectionSemesterCourses? sectionInfo { get; set; }

        public DateTime Date { get; set; }

        public List<StudentSectionAttendance>? Attendances { get; set; }
    }
}
