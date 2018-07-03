namespace SoftUni.WebServer.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ProductType
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public IList<Product> Products { get; set; } = new List<Product>();
    }
}