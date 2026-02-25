using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClassroomManagementApp1.Models
{
    public class Subject
    {
        [Key]
        public string subjectid { get; set; }

        public string subjectname { get; set; } = string.Empty; // Non-nullable

        public int capacity { get; set; }

        public string description { get; set; } = string.Empty; // Non-nullable

        // Navigation Property
        public ICollection<Class> Classes { get; set; } = new List<Class>(); // Initialize to avoid null reference
    }
}
