using BookShare.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;




namespace BookShare
{
    public class ApplicationDbContext : IdentityDbContext 
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
    public DbSet<Book> Books { get; set; }
    }

    
    

}