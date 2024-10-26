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
        public string studentid { get; set; }  // Thuộc tính khóa chính

        public string studentemail { get; set; }

        public string studentname { get; set; }

        public int studentgrade { get; set; }

        // Liên kết 1-n với ClassStudent
        public ICollection<ClassStudent> ClassStudents { get; set; } = new List<ClassStudent>();

        // Liên kết 1-n với Submission
        public ICollection<Submission> Submissions { get; set; } = new List<Submission>();

        public ICollection<Account> Accounts { get; set; } = new List<Account>();
    }
}
