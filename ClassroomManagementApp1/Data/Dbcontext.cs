using Microsoft.EntityFrameworkCore; // Changed from System.Data.Entity to Microsoft.EntityFrameworkCore
using ClassroomManagementApp1.Models;

namespace ClassroomManagementApp1.Data
{
    public class AppDbContext : DbContext
    {
        // Default constructor using the connection string name from App.config or other configurations
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSets corresponding to models
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<ClassStudent> ClassStudent { get; set; }
        public DbSet<Subject> Subject { get; set; }
        public DbSet<Assignment> Assignment { get; set; }
        public DbSet<Submission> Submissions { get; set; }

        // Model configuration and table mapping
        protected override void OnModelCreating(ModelBuilder modelBuilder) // Changed from DbModelBuilder to ModelBuilder
        {
            // Map table "student"
            modelBuilder.Entity<Student>().ToTable("student", "public");

            // Map table "teacher"
            modelBuilder.Entity<Teacher>().ToTable("teacher", "public");

            // Map table "class"
            modelBuilder.Entity<Class>().ToTable("class", "public");

            // Map table "assignment"
            modelBuilder.Entity<Assignment>().ToTable("assignment", "public");

            // Map table "classstudent"
            modelBuilder.Entity<ClassStudent>().ToTable("classstudent", "public");

            // Map table "account"
            modelBuilder.Entity<Account>().ToTable("account", "public");

            // Map table "subject"
            modelBuilder.Entity<Subject>().ToTable("subject", "public");

            // Map table "submission"
            modelBuilder.Entity<Submission>().ToTable("submission", "public");

            // Set primary key for ClassStudent (many-to-many)
            modelBuilder.Entity<ClassStudent>()
                .HasKey(cs => new { cs.classid, cs.studentid });
        }
    }
}
