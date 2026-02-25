using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassroomManagement.Domain.Entities;

public class Class
{
    [Key]
    public string classid { get; set; } = null!;

    public string teacherid { get; set; } = null!;

    public string subjectid { get; set; } = null!;

    public string classname { get; set; } = null!;

    public DateTime datebegin { get; set; }

    public DateTime dateend { get; set; }

    public ICollection<ClassStudent> ClassStudents { get; set; } = new List<ClassStudent>();

    [ForeignKey("subjectid")]
    public Subject Subject { get; set; } = null!;

    [ForeignKey("teacherid")]
    public Teacher Teacher { get; set; } = null!;

    public ICollection<Assignment> Assignments { get; set; } = new List<Assignment>();
}

