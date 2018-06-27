namespace KittensApp.WebIt.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using KittenApp.Models;
    using Microsoft.EntityFrameworkCore;
    using Models.BindingModels;
    using Models.ViewModels;
    using Services;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Attributes.Security;
    using SimpleMvc.Framework.Interfaces;

    public class KittensController : BaseController
    {
        private KittensService kittens;

        public KittensController()
        {
            this.kittens = new KittensService();
        }

        [PreAuthorize]
        public IActionResult Add()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            return View();
        }

        [HttpPost]
        [PreAuthorize]
        public IActionResult Add(KittenAddingModel model)
        {
            if (!this.User.IsAuthenticated)
            {
                return RedirectToHome();
            }

            if (!this.IsValidModel(model))
            {
                SetValidatorErrors();
                return this.View();
            }

            Breed breed = this.kittens.GetBreed(model.Breed);

            if (breed == null)
            {
                this.ShowError("Invalid Breed!");
                return this.View();
            }

            var kitten = this.kittens.Create(model.Name, model.Age, breed);
            if (kitten == null)
            {
                ShowError("Unsuccessfull operation!");
                return this.View();
            }

            return RedirectToAction("/kittens/all");
        }

        [HttpGet]
        [PreAuthorize]
        public IActionResult All()
        {
            var kittens = PrepareKittensDetails();
            var kittensResult = PrepareHtmlDetails(kittens);

            this.Model.Data["allKittens"] = kittensResult.ToString();

            return this.View();
        }

        private static StringBuilder PrepareHtmlDetails(List<string> kittens)
        {
            var kittensResult = new StringBuilder();
            kittensResult.Append(@"<div class=""row text-center"">");
            for (int i = 0; i < kittens.Count; i++)
            {
                kittensResult.Append(kittens[i]);

                if (i % 3 == 2)
                {
                    kittensResult.Append(@"</div><div class=""row text-center"">");
                }
            }

            kittensResult.Append("</div>");
            return kittensResult;
        }

        private List<string> PrepareKittensDetails()
        {
            return this.Context.Kittens
                            .Include(k => k.Breed)
                            .Select(KittenDetailsViewModel.FromKitten)
                            .Select(kdvm =>
                                $@"<div class=""col-4"">
                                    <img class=""img-thumbnail"" src={kdvm.PictureUrl} alt=""{kdvm.Name}'s photo"" />
                                        <div>
                                            <h5>Name: {kdvm.Name}</h5>
                                            <h5>Age: {kdvm.Age}</h5>
                                            <h5>Breed: {kdvm.Breed}</h5>
                                        </div>
                                    </div>")
                            .ToList();
        }
    }
}
