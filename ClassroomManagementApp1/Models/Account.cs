using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using ClassroomManagementApp1.Models;

namespace ClassroomManagementApp1.Models
{
    public class Account
    {
        [Key]
        public string userid { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        // Foreign key specification
        [ForeignKey("Student")]
        public string studentid { get; set; }
        public string role { get; set; }

        // Reference to Student
        public Student Student { get; set; }
        public byte[] profilepicture { get; set; }
    }
}
