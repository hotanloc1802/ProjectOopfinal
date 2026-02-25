using ClassroomManagementApp1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassroomManagementApp1.Models
{
    public class Student
    {
        [Key]
        public string studentid { get; set; }  // Primary key property

        public string studentname { get; set; }

        public string studentgrade { get; set; }

        public string studentemail { get; set; }

        public string studentbirth { get; set; }

        // One-to-many relationship with ClassStudent
        public ICollection<ClassStudent> ClassStudents { get; set; } = new List<ClassStudent>();

        // One-to-many relationship with Submission
        public ICollection<Submission> Submissions { get; set; } = new List<Submission>();

        // One-to-many relationship with Account
        public ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}
