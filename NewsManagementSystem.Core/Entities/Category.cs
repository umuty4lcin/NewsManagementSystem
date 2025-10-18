using System.ComponentModel.DataAnnotations;

namespace NewsManagementSystem.Core.Entities
{
    public class Category : BaseEntity
    {
        [Required(ErrorMessage = "Kategori adı gereklidir")]
        [StringLength(100, ErrorMessage = "Kategori adı en fazla 100 karakter olabilir")]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(500, ErrorMessage = "Açıklama en fazla 500 karakter olabilir")]
        public string? Description { get; set; }
        
        // Navigation property
        public ICollection<News>? NewsList { get; set; }
    }
}

