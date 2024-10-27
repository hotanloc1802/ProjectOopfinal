using ClassroomManagementApp1.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassroomManagementApp1.Models
{
    public class ClassStudent
    {
        // Composite Key
        [Key, Column(Order = 0)]
        [ForeignKey("Class")]
        public string classid { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("Student")]
        public string studentid { get; set; }

        // Navigation Properties
        public Class Class { get; set; }
        public Student Student { get; set; }
    }
}
