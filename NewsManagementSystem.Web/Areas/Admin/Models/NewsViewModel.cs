using System.ComponentModel.DataAnnotations;

namespace NewsManagementSystem.Web.Areas.Admin.Models
{
    public class NewsViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Başlık gereklidir")]
        [StringLength(200, ErrorMessage = "Başlık en fazla 200 karakter olabilir")]
        [Display(Name = "Başlık")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Özet gereklidir")]
        [StringLength(500, ErrorMessage = "Özet en fazla 500 karakter olabilir")]
        [Display(Name = "Özet")]
        public string Summary { get; set; }

        [Required(ErrorMessage = "İçerik gereklidir")]
        [Display(Name = "İçerik")]
        public string Content { get; set; }

        [Required(ErrorMessage = "Yazar gereklidir")]
        [StringLength(100, ErrorMessage = "Yazar en fazla 100 karakter olabilir")]
        [Display(Name = "Yazar")]
        public string Author { get; set; }

        [Required(ErrorMessage = "Kategori seçiniz")]
        [Range(1, int.MaxValue, ErrorMessage = "Lütfen bir kategori seçiniz")]
        [Display(Name = "Kategori")]
        public int CategoryId { get; set; }

        public string? CurrentImageUrl { get; set; }
    }
}

