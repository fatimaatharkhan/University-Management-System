using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flex.Models
{
    public class Semester
    {
        [System.ComponentModel.DataAnnotations.Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        [DisplayName("Semester Name")]
        public string Name { get; set; }

        [MaxLength(200)]
        [DisplayName("Description")]
        public string? Description { get; set; }

        [DisplayName("Start Date")]
        public DateTime startDate { get; set; }

        [DisplayName("End Date")]
        public DateTime endDate { get; set; }

        [NotMapped]
        public int CourseId { get; set; }
        public List<SemesterCourses>? SemesterCourses { get; set; }
       

    }
}
