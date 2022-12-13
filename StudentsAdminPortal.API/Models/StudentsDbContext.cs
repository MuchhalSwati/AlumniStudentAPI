using Microsoft.EntityFrameworkCore;

namespace StudentsAdminPortal.API.Models
{
    public class StudentsDbContext : DbContext
    {
        public StudentsDbContext(DbContextOptions<StudentsDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Credits> Credits { get; set; }
        public virtual DbSet<ContactInfo> ContactInfo { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<University> University { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<University>().Navigation(c => c.Department).AutoInclude();
            modelBuilder.Entity<Department>().Navigation(j => j.Student).AutoInclude();
            modelBuilder.Entity<Student>().Navigation(k => k.Credits).AutoInclude();
            modelBuilder.Entity<Student>().Navigation(l => l.ContactInfo).AutoInclude();
            modelBuilder.Entity<Student>()
                .HasOne(b => b.Credits)
                .WithOne(i => i.Student)
                .HasForeignKey<Credits>(b => b.StudentId);

            modelBuilder.Entity<Student>()
               .HasOne(b => b.ContactInfo)
               .WithOne(i => i.Student)
               .HasForeignKey<ContactInfo>(b => b.StudentId);

            modelBuilder.Entity<Student>()
            .HasOne(p => p.Department)
            .WithMany(b => b.Student);
           
        }

    }


}
