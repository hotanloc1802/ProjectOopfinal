using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassroomManagement.Domain.Entities;

public class Account
{
    [Key]
    public string userid { get; set; } = null!;

    public string username { get; set; } = null!;

    public string password { get; set; } = null!;

    [ForeignKey("Student")]
    public string studentid { get; set; } = null!;

    public string role { get; set; } = null!;

    public Student Student { get; set; } = null!;

    public byte[]? profilepicture { get; set; }
}

