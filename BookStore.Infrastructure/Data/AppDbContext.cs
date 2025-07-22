using BookStore.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Book - Category ilişkisi
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Books)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Author - Value Object
            modelBuilder.Entity<Book>()
                .OwnsOne(b => b.Author, a =>
                {
                    a.Property(p => p.FirstName).HasColumnName("AuthorFirstName").IsRequired();
                    a.Property(p => p.LastName).HasColumnName("AuthorLastName").IsRequired();
                });


            // Price - Value Object
            modelBuilder.Entity<Book>()
                .OwnsOne(b => b.Price, p =>
                {
                    p.Property(x => x.Amount).HasColumnName("PriceAmount").IsRequired();
                    p.Property(x => x.Currency).HasColumnName("PriceCurrency").IsRequired();
                });
        }
    }

}
