namespace SoftUni.WebServer.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Order
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        public decimal SalePrice { get; set; }

        [Required]
        public DateTime OrderedOn { get; set; }
    }
}
