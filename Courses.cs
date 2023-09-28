using MessagePack;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Flex.Models
{
    public class Courses
    {
        [System.ComponentModel.DataAnnotations.Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int courseID { get; set; }

        [Required]
        [MaxLength(15)]
        [DisplayName("Course Code")]
        public string courseCode { get; set; }
        
        [Required]
        [MaxLength(200)]
        [DisplayName("Course Name")]
        public string courseName { get; set; }

        [MaxLength(500)]
        [DisplayName("Course Description")]
        public string courseDescription { get; set; }

        [Required]
        [DisplayName("Credit Hours")]
        public int creditHours { get; set; }

        [DisplayName("Pre-requisite Course")]
        public int? preReq_ID { get; set; }

        [ForeignKey("preReq_ID")]
        public Courses? PreReqs { get; set; }
    }
}
