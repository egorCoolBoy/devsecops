using Microsoft.EntityFrameworkCore;

namespace WebApplication5.Data;

public class StudentContext : DbContext
{
    public StudentContext(DbContextOptions<StudentContext> options) : base(options) {}
    public DbSet<Student> Students { get; set; }
}