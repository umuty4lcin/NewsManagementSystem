using System.ComponentModel.DataAnnotations;

namespace NewsManagementSystem.Web.Areas.Admin.Models
{
    public class ForgotPasswordViewModel
    {
        [Required(ErrorMessage = "Email adresi gereklidir")]
        [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz")]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}

