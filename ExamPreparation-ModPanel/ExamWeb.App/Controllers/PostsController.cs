namespace ExamWeb.App.Controllers
{
    using Infrastructure;
    using Models.BindingModels;
    using Services;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;

    public class PostsController : BaseController
    {
        private PostsService posts;

        public PostsController()
        {
            this.posts = new PostsService();
        }

        [HttpGet]
        public IActionResult Add()
        {
            if (!this.User.IsAuthenticated || !this.Profile.IsApproved)
            {
                return RedirectToHome();
            }

            return this.View();
        }

        [HttpPost]
        public IActionResult Add(PostCreatingModel model)
        {
            if (!this.User.IsAuthenticated || !this.Profile.IsApproved)
            {
                return RedirectToHome();
            }

            if (!this.IsValidModel(model))
            {
                SetValidatorErrors();
                return this.View();
            }

            var success = this.posts.Create(model.Title, model.Content, this.Profile.Id);
            if (!success)
            {
                ShowError(Constants.UnsuccessfullOperationMessage);
                return this.View();
            }

            return RedirectToHome();
        }
    }
}
