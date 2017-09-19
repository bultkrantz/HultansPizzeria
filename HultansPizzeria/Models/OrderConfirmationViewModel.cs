namespace HultansPizzeria.Models
{
    public class OrderConfirmationViewModel
    {
        public Cart Cart { get; set; }

        public string Address { get; set; }

        public int ApartmentNumber { get; set; }

        public int Floor { get; set; }

        public int EntryCode { get; set; }

        public string DeliveryMethod { get; set; }

        public string PaymentMethod { get; set; }
    }
}
