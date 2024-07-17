using BookShare.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;




namespace BookShare
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }
    public DbSet<Book> Books { get; set; }
    public DbSet<Event> Events { get; set; }

    public DbSet<Reviews> Review { get; set; }
    
    }

    
    

}