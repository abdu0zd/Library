using System.Diagnostics;
using Library.Entities;
using Microsoft.EntityFrameworkCore;


namespace Library.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<Book> books { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Borrow> borrow { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=DESKTOP-4DGV9IR;Database=Library;Trusted_Connection=True;trustservercertificate=true");
        //}

        //Dependency Injection in AppDbcontext
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Book>().Property(i => i.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Book>().HasKey(i=>i.Id);
            modelBuilder.Entity<User>().Property(i => i.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<User>().HasKey(i => i.Id);
            modelBuilder.Entity<Borrow>().Property(i => i.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<Borrow>().HasKey(i => i.Id);
            //modelBuilder.Entity<Borrow>().HasKey(i => i.BookId);
            //modelBuilder.Entity<Borrow>().HasKey(i => i.UserId);
            modelBuilder.Entity<Borrow>().HasOne(i=>i.Book).WithMany(i=>i.BorrowUser).HasForeignKey(i=>i.BookId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Borrow>().HasOne(i=>i.User).WithMany(i=>i.BorrowBooks).HasForeignKey(i=>i.UserId).OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

    }
}
