namespace ExamWeb.App.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class PostCreatingModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MinLength(3)]
        public string Content { get; set; }
    }
}
