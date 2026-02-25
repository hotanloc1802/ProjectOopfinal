using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassroomManagement.Domain.Entities;

public class ClassStudent
{
    [Key, Column(Order = 0)]
    [ForeignKey("Class")]
    public string classid { get; set; } = null!;

    [Key, Column(Order = 1)]
    [ForeignKey("Student")]
    public string studentid { get; set; } = null!;

    public Class Class { get; set; } = null!;

    public Student Student { get; set; } = null!;
}

