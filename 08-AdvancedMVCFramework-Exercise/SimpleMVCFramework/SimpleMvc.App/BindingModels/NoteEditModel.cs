namespace SimpleMvc.App.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class NoteEditModel
    {
        [Required]
        [MinLength(3)]
        public string Title { get; set; }

        [Required]
        [MinLength(4)]
        public string Content { get; set; }
    }
}
