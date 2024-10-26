using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassroomManagementApp1.Models
{
    public class Teacher
    {
        [Key]
        public string teacherid { get; set; }

        public string teachername { get; set; }

        public string teacheremail { get; set; }

        // Navigation Property - one-to-many relationship with Class
        public ICollection<Class> Classes { get; set; } = new List<Class>(); // Initialize to avoid null reference
    }
}
