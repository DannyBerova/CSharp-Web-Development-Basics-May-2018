namespace HttpServer.GameStoreApplication.ViewModels.Admin
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using HttpServer.GameStoreApplication.Common;

    public class AdminEditGameViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(VaidationConstants.Game.TitleMinLength,
            ErrorMessage = VaidationConstants.IvalidMinLegnthErrorMessage)]
        [MaxLength(VaidationConstants.Game.TitleMaxLength,
            ErrorMessage = VaidationConstants.IvalidMaxLegnthErrorMessage)]
        public string Title { get; set; }

        [Display(Name = "Youtube Video URL")]
        [Required]
        [MinLength(VaidationConstants.Game.VideoIdLength,
            ErrorMessage = VaidationConstants.ExactLegnthErrorMessage)]
        [MaxLength(VaidationConstants.Game.VideoIdLength,
            ErrorMessage = VaidationConstants.ExactLegnthErrorMessage)]
        public string VideoId { get; set; }

        [Required]
        public string Image { get; set; }

        //In GB
        public double Size { get; set; }

        public decimal Price { get; set; }

        [Required]
        [MinLength(VaidationConstants.Game.DescriptionMinLength,
            ErrorMessage = VaidationConstants.IvalidMinLegnthErrorMessage)]
        public string Description { get; set; }

        [Display(Name = "Release Date")]
        [Required]
        public DateTime? ReleaseDate { get; set; }
    }
}
