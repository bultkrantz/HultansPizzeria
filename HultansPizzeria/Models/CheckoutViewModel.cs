using System.ComponentModel.DataAnnotations;

namespace HultansPizzeria.Models
{
    public class CheckoutViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }

        public ApplicationUser User { get; set; }
    }
}
