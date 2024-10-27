using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassroomManagementApp1.Models
{
    public class Submission
    {
        [Key]
        public string submissionid { get; set; }

        [ForeignKey("Assignment")]
        public string assignmentid { get; set; }

        [ForeignKey("Student")]
        public string studentid { get; set; }

        [Range(0, 100, ErrorMessage = "Score must be between 0 and 100")]
        public int score { get; set; }

        // Navigation Properties
        public Assignment Assignment { get; set; }
        public Student Student { get; set; }
    }
}
