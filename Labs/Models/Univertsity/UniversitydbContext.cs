using Microsoft.EntityFrameworkCore;
using static Lab1.Models.LibrarydbContext;

namespace Lab1.Models;

public class UniversitydbContext : DbContext
{
    public UniversitydbContext(DbContextOptions<UniversitydbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Course> Courses { get; set; }
    public DbSet<Student> Students { get; set; }
    public DbSet<StudentsCourses> StudentsCourses { get; set; }
    public DbSet<Faculty> Faculties { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=Universitydb;Username=hadi;Password=123");

}