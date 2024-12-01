using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomManagementApp1.Data
{
    public class StudentContext
    {
        private static StudentContext _instance;

        // Property to store studentId
        public string StudentId { get; private set; }

        // Initialize singleton
        private StudentContext() { }

        public static StudentContext Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StudentContext();
                }
                return _instance;
            }
        }

        // Method to set studentId
        public void SetStudentId(string studentId)
        {
            StudentId = studentId;
        }
    }
}
