using ClassroomManagementApp1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassroomManagementApp1.Models
{
    public class Class
    {
        [Key]
        public string classid { get; set; }

        // Foreign key for Teacher
        public string teacherid { get; set; }

        // Foreign key for Subject
        public string subjectid { get; set; }

        public string classname { get; set; }
        public DateTime datebegin { get; set; }
        public DateTime dateend { get; set; }

        // Navigation Property for Class-Student relationship (1-n)
        public ICollection<ClassStudent> ClassStudents { get; set; }

        // Navigation Property for Subject
        [ForeignKey("subjectid")]
        public Subject Subject { get; set; }

        // Navigation Property for Teacher
        [ForeignKey("teacherid")]
        public Teacher Teacher { get; set; }

        // Navigation Property for Assignment (1-n)
        public ICollection<Assignment> Assignments { get; set; }
    }
}
