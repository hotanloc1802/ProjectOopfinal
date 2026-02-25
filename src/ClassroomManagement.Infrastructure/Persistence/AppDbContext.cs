using ClassroomManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClassroomManagement.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<Teacher> Teachers { get; set; } = null!;
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Class> Classes { get; set; } = null!;
    public DbSet<ClassStudent> ClassStudent { get; set; } = null!;
    public DbSet<Subject> Subject { get; set; } = null!;
    public DbSet<Assignment> Assignment { get; set; } = null!;
    public DbSet<Submission> Submissions { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>().ToTable("student", "public");
        modelBuilder.Entity<Teacher>().ToTable("teacher", "public");
        modelBuilder.Entity<Class>().ToTable("class", "public");
        modelBuilder.Entity<Assignment>().ToTable("assignment", "public");
        modelBuilder.Entity<ClassStudent>().ToTable("classstudent", "public");
        modelBuilder.Entity<Account>().ToTable("account", "public");
        modelBuilder.Entity<Subject>().ToTable("subject", "public");
        modelBuilder.Entity<Submission>().ToTable("submission", "public");

        modelBuilder.Entity<ClassStudent>()
            .HasKey(cs => new { cs.classid, cs.studentid });

        base.OnModelCreating(modelBuilder);
    }
}

