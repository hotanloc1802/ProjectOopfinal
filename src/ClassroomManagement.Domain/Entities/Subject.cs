using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClassroomManagement.Domain.Entities;

public class Subject
{
    [Key]
    public string subjectid { get; set; } = null!;

    public string subjectname { get; set; } = string.Empty;

    public int capacity { get; set; }

    public string description { get; set; } = string.Empty;

    public ICollection<Class> Classes { get; set; } = new List<Class>();
}

