namespace NewsManagementSystem.Core.Entities
{
    public class News : BaseEntity
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public string Author { get; set; }
        public int ViewCount { get; set; }
        public bool IsPublished { get; set; }
        public DateTime? PublishedDate { get; set; }
        
        // Foreign Keys
        public int CategoryId { get; set; }
        public string UserId { get; set; }
        
        // Navigation properties
        public Category Category { get; set; }
        public AppUser User { get; set; }
    }
}

