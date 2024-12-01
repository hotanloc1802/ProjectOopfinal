using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassroomManagementApp1.DesignPattern
{
    public class StudentContextSingleton
    {
        private static StudentContextSingleton _instance;

        // Property to store studentId
        public string StudentId { get; private set; }

        // Initialize singleton
        private StudentContextSingleton() { }

        public static StudentContextSingleton Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StudentContextSingleton();
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
