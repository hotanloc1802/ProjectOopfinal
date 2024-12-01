using Microsoft.EntityFrameworkCore;
using ClassroomManagementApp1.Data;
using ClassroomManagementApp1.ClassService;

namespace ClassroomManagementApp1.Factory
{
    public static class ServiceFactory
    {
        private static AppDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=uit;Username=postgres;Password=123123zzA.;SearchPath=OOP-new,public;");
            return new AppDbContext(optionsBuilder.Options);
        }

        public static ClassesService CreateClassesService()
        {
            var context = CreateDbContext();
            return new ClassesService(context);
        }

        public static AssignmentService CreateAssignmentService()
        {
            var context = CreateDbContext();
            return new AssignmentService(context);
        }

        public static SubmissionService CreateSubmissionService()
        {
            var context = CreateDbContext();
            return new SubmissionService(context);
        }
        public static TeacherService CreateTeacherService()
        {
            var context = CreateDbContext();
            return new TeacherService(context);
        }
        public static StudentService CreateStudentService()
        {
            var context = CreateDbContext();
            return new StudentService(context);
        }
        public static AccountService CreateAccountService()
        {
            var context = CreateDbContext();
            return new AccountService(context);
        }

    }
}
