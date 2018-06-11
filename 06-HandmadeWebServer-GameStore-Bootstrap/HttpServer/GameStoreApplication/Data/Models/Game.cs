namespace HttpServer.GameStoreApplication.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using HttpServer.GameStoreApplication.Common;

    public class Game
    {
        public int Id { get; set; }

        [Required]
        [MinLength(VaidationConstants.Game.TitleMinLength,
            ErrorMessage = VaidationConstants.IvalidMinLegnthErrorMessage)]
        [MaxLength(VaidationConstants.Game.TitleMaxLength,
            ErrorMessage = VaidationConstants.IvalidMaxLegnthErrorMessage)]
        public string Title { get; set; }

        [Required]
        [MinLength(VaidationConstants.Game.VideoIdLength,
            ErrorMessage = VaidationConstants.ExactLegnthErrorMessage)]
        [MaxLength(VaidationConstants.Game.VideoIdLength,
            ErrorMessage = VaidationConstants.ExactLegnthErrorMessage)]
        public string VideoId { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        //In GB
        public double Size { get; set; }

        public decimal Price { get; set; }

        [Required]
        [MinLength(VaidationConstants.Game.DescriptionMinLength,
            ErrorMessage = VaidationConstants.IvalidMinLegnthErrorMessage)]
        public string Description { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        public List<UserGame> Users { get; set; } = new List<UserGame>();
    }
}
