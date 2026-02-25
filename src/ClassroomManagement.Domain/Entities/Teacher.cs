using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClassroomManagement.Domain.Entities;

public class Teacher
{
    [Key]
    public string teacherid { get; set; } = null!;

    public string teachername { get; set; } = null!;

    public string teacheremail { get; set; } = null!;

    public ICollection<Class> Classes { get; set; } = new List<Class>();
}

