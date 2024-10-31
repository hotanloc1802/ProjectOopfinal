using Microsoft.EntityFrameworkCore; // Thay đổi từ System.Data.Entity sang Microsoft.EntityFrameworkCore
using ClassroomManagementApp1.Models;

namespace ClassroomManagementApp1.Data
{
    public class AppDbContext : DbContext
    {
        // Constructor mặc định sử dụng tên chuỗi kết nối từ App.config hoặc cấu hình khác
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Các DbSet tương ứng với các models
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassStudent> ClassStudent { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<Assignment> Assignment { get; set; }
        public DbSet<Submission> Submissions { get; set; }

        // Cấu hình model và ánh xạ bảng
        protected override void OnModelCreating(ModelBuilder modelBuilder) // Thay đổi từ DbModelBuilder sang ModelBuilder
        {
            // Ánh xạ bảng student
            modelBuilder.Entity<Student>().ToTable("student", "public");

            // Ánh xạ bảng teacher
            modelBuilder.Entity<Teacher>().ToTable("teacher", "public");

            // Ánh xạ bảng class
            modelBuilder.Entity<Class>().ToTable("class", "public");

            // Ánh xạ bảng assignment
            modelBuilder.Entity<Assignment>().ToTable("assignment", "public");

            // Ánh xạ bảng classstudent
            modelBuilder.Entity<ClassStudent>().ToTable("classstudent", "public");

            // Ánh xạ bảng account
            modelBuilder.Entity<Account>().ToTable("account", "public");

            // Ánh xạ bảng subject
            modelBuilder.Entity<Subject>().ToTable("subject", "public");

            // Ánh xạ bảng submission
            modelBuilder.Entity<Submission>().ToTable("submission", "public");

            // Thiết lập khóa chính cho ClassStudent (nhiều-nhiều)
            modelBuilder.Entity<ClassStudent>()
                .HasKey(cs => new { cs.classid, cs.studentid });
        }
    }
}
