namespace KittensApp.WebIt.Models.ViewModels
{
    using System;
    using System.Linq.Expressions;
    using KittenApp.Models;

    public class KittenDetailsViewModel
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public string Breed { get; set; }

        public string PictureUrl { get; set; } = "https://vignette.wikia.nocookie.net/beastwarstransformers/images/a/a1/Cat-adult-landing-hero.jpg/revision/latest?cb=20170705171807";

        public static Expression<Func<Kitten, KittenDetailsViewModel>> FromKitten =>
            k => new KittenDetailsViewModel()
            {
                Name = k.Name,
                Age = k.Age,
                Breed = k.Breed.Name
            };
    }
}
