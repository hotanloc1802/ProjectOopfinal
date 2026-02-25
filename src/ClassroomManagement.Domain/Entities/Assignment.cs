using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassroomManagement.Domain.Entities;

public class Assignment
{
    [Key]
    public string assignmentid { get; set; } = null!;

    [ForeignKey("Class")]
    public string classid { get; set; } = null!;

    public string description { get; set; } = string.Empty;

    public DateTime duedate { get; set; }

    public ICollection<Submission> Submissions { get; set; } = new List<Submission>();

    public Class Class { get; set; } = null!;

    [NotMapped]
    public int status { get; set; }

    public void UpdateStatus()
    {
        status = duedate >= DateTime.Now ? 1 : 0;
    }
}

