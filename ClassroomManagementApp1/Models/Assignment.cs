using ClassroomManagementApp1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomManagementApp1.Models
{
    public class Assignment
    {
        [Key]
        public string assignmentid { get; set; }
        [ForeignKey("Class")]
        public string classid { get; set; }
        public string description { get; set; } = string.Empty; // Non-nullable
        public DateTime duedate { get; set; }

        public ICollection<Submission> Submissions { get; set; }

        public Class Class { get; set; }
    }
}
