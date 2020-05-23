using Microsoft.EntityFrameworkCore;

namespace TodoApi.Models
{
    public class TodoContext : DbContext
    {
        public TodoContext(DbContextOptions<TodoContext> options)
            : base(options)
        {
        }

        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<Company> Company { get; set; }
        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Company>()
                .HasMany(c => c.Employees)
                .WithOne(e => e.Company)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);  //jika menghapus record yang di table company sedangkan id company masih dipakakai di table employee maka semua record di table employee yang memakai id company tersebut akan ikut terhapus
            */
            modelBuilder.Entity<Company>()
                .HasMany(c => c.Employees)
                .WithOne(e => e.Company)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);  //jika menghapus record yang di table company sedangkan id company masih dipakakai di table employee maka akan muncul pesan error, dan penghapusan gagal


            //DATA SEEDING
            modelBuilder.Entity<Company>().HasData(
                new Company() { Id=1, Name = "Test 1" },
                new Company() { Id = 2, Name = "Test 2" },
                new Company() { Id = 3, Name = "Test 3" }
            );
        }
    }
}