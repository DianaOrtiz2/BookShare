using Microsoft.AspNetCore.Identity;

namespace BookShare.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string Fullname {get; set;}
        public DateTime BirthDate {get; set;}
        public string gender {get; set;}
    }
}