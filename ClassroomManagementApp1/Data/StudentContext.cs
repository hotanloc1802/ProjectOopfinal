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

        // Thuộc tính để lưu trữ studentId
        public string StudentId { get; private set; }

        // Khởi tạo singleton
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

        // Phương thức để đặt studentId
        public void SetStudentId(string studentId)
        {
            StudentId = studentId;
        }
    }

}
