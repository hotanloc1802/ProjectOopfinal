using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassroomManagement.Domain.Entities;

public class Submission
{
    [Key]
    public string submissionid { get; set; } = null!;

    [ForeignKey("Assignment")]
    public string assignmentid { get; set; } = null!;

    [ForeignKey("Student")]
    public string studentid { get; set; } = null!;

    [Range(0, 100, ErrorMessage = "Score must be between 0 and 100")]
    public double score { get; set; }

    public string linksubmisson { get; set; } = null!;

    public Assignment Assignment { get; set; } = null!;

    public Student Student { get; set; } = null!;
}

