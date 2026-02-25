using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClassroomManagement.Domain.Entities;

public class Student
{
    [Key]
    public string studentid { get; set; } = null!;

    public string studentname { get; set; } = null!;

    public string studentgrade { get; set; } = null!;

    public string studentemail { get; set; } = null!;

    public string studentbirth { get; set; } = null!;

    public ICollection<ClassStudent> ClassStudents { get; set; } = new List<ClassStudent>();

    public ICollection<Submission> Submissions { get; set; } = new List<Submission>();

    public ICollection<Account> Accounts { get; set; } = new List<Account>();
}

