using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BussinessLayer;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BusinessLayer;

namespace DataLayer
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {

                optionsBuilder.UseSqlServer(@"Server=localhost\SQLEXPRESS; Database=TESTBNT;Trusted_Connection=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRole>().HasData(
                 new IdentityRole("Admin") { NormalizedName = "ADMIN" },
                 new IdentityRole("Editor") { NormalizedName = "EDITOR" },
                 new IdentityRole("User") { NormalizedName = "USER" }
                 );

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Video> Videos { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<Format> Formats { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<User> Users { get; set; }

        
    }
}
