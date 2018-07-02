namespace ExamWeb.App.Controllers
{
    using System.Linq;
    using ExamWeb.Models;
    using Infrastructure;
    using Services;
    using SimpleMvc.Framework.Attributes.Methods;
    using SimpleMvc.Framework.Interfaces;

    public class AdminController : BaseController
    {
        private readonly UsersService users;
        private readonly PostsService posts;
        private readonly LogsService logs;

        public AdminController()
        {
            this.users = new UsersService();
            this.posts = new PostsService();
            this.logs = new LogsService();
        }

        [HttpGet]
        public IActionResult Users()
        {
            if (!this.IsAdmin)
            {
                return this.RedirectToLogin();
            }

            var rows = this.users
                .All()
                .Select(u => u.ToHtml());

            this.Model["users"] = string.Join(string.Empty, rows);

            this.Log(LogType.OpenMenu, nameof(Users));

            return this.View();
        }

        [HttpGet]
        public IActionResult Approve(int id)
        {
            if (!this.IsAdmin)
            {
                return this.RedirectToLogin();
            }

            var userEmail = this.users.Approve(id);

            if (userEmail != null)
            {
                 this.Log(LogType.UserApproval, userEmail);
            }

            return this.RedirectToAction("/admin/users");
        }

        [HttpGet]
        public IActionResult Posts()
        {
            if (!this.IsAdmin)
            {
                return this.RedirectToLogin();
            }

            var rows = this.posts
                .All()
                .Select(p => p.ToHtml());

            this.Model["posts"] = string.Join(string.Empty, rows);

            this.Log(LogType.OpenMenu, nameof(Posts));

            return this.View();
        }

        public IActionResult Edit(int id)
            => this.PrepareEditAndDeleteView(id)
                ?? this.View();

        [HttpPost]
        public IActionResult Edit(int id, Post model)
        {
            if (!this.IsAdmin)
            {
                return this.RedirectToLogin();
            }

            if (!this.IsValidModel(model))
            {
                SetValidatorErrors();
                return this.View();
            }

            this.posts.Update(id, model.Title, model.Content);

            this.Log(LogType.EditPost, model.Title);

            return this.RedirectToAction("/admin/posts");
        }

        public IActionResult Delete(int id)
        {
            this.Model["id"] = id.ToString();

            return this.PrepareEditAndDeleteView(id) ?? this.View();
        }

        [HttpPost]
        public IActionResult Confirm(int id)
        {
            if (!this.IsAdmin)
            {
                return this.RedirectToLogin();
            }

            var postTitle = this.posts.Delete(id);

            if (postTitle != null)
            {
                this.Log(LogType.DeletePost, postTitle);
            }

            return this.RedirectToAction("/admin/posts");
        }

        [HttpGet]
        private IActionResult PrepareEditAndDeleteView(int id)
        {
            if (!this.IsAdmin)
            {
                return this.RedirectToLogin();
            }

            var post = this.posts.GetById(id);

            if (post == null)
            {
                return this.RedirectToHome();
            }

            this.Model["title"] = post.Title;
            this.Model["content"] = post.Content;

            return null;
        }

        [HttpGet]
        public IActionResult Log()
        {
            this.Log(LogType.OpenMenu, nameof(Log));

            var rows = this.logs
                .All()
                .Select(l => l.ToHtml());

            this.Model["logs"] = string.Join(string.Empty, rows);

            return this.View();
            
        }
    }
}
