using HultansPizzeria.Models.AccountViewModels;
using System;
using System.ComponentModel.DataAnnotations;

namespace HultansPizzeria.Models
{
    public class CheckoutViewModel
    {
        public LoginViewModel LoginViewModel { get; set; }
        public ApplicationUser User { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [Display(Name = "Adress")]
        public string Address { get; set; }

        [Required]
        [Range(0, 99999, ErrorMessage = "Apartmentnumber too big.")]
        [Display(Name = "Lägenhetsnummer")]
        public int ApartmentNumber { get; set; }

        [Required]
        [Range(0, 99, ErrorMessage = "Not a valid floornumber")]
        [Display(Name = "Våning")]
        public int Floor { get; set; }

        [Range(0, 99999, ErrorMessage = "Entrycode too big.")]
        [Display(Name = "Portkod")]
        public int EntryCode { get; set; }

        [Required]
        public string DeliveryMethod { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        [Required]
        [DataType(DataType.CreditCard)]
        [Display(Name = "Kortnummer")]
        public string CardNumber { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Utgångsdatum")]
        public DateTime ExpireDate { get; set; }

        [Required]
        [StringLength(3, MinimumLength = 3, ErrorMessage = "Not a valid CCV")]
        [Display(Name = "CCV")]
        public string CCV { get; set; }
    }
}
