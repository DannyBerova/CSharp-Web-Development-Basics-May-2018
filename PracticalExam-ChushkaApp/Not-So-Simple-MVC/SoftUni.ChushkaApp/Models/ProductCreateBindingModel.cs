namespace SoftUni.ChushkaApp.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ProductCreateBindingModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [MinLength(3)]
        public string Description { get; set; }

        public int Type { get; set; }
    }
}
