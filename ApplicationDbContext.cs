using BookShare.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookShare
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
    public DbSet<Book> Books { get; set; }
    }
}