using Microsoft.AspNetCore.Identity;

namespace NewsManagementSystem.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        
        // Navigation property
        public ICollection<News> NewsList { get; set; }
    }
}

