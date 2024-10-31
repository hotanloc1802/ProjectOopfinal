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
        // Khóa ngoại chỉ định
        [ForeignKey("Student")]
        public string studentid { get; set; }

        // Tham chiếu đến Student
        public Student Student { get; set; }
    }
}
