namespace KittensApp.WebIt.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class KittenAddingModel
    {
        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [Range(1, 20)]
        public int Age { get; set; }

        public string Breed { get; set; }
    }
}
