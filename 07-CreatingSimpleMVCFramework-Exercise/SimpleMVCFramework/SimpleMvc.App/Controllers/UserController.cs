namespace SimpleMvc.App.Controllers
{
    using System.Collections.Generic;
    using Models;
    using Services;
    using SimpleMVC.Framework.Attributes.Methods;
    using SimpleMVC.Framework.Controllers;
    using SimpleMVC.Framework.Interfaces;
    using SimpleMVC.Framework.Interfaces.Generic;
    using SimpleMVC.Framework.ViewEngine.Generic;
    using Views.User;

    public class UserController : Controller
    {
        private readonly UserService userService;
        private readonly NoteService noteService;

        public UserController()
        {
            this.userService = new UserService();
            this.noteService = new NoteService();
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        public IActionResult<IEnumerable<UserViewModel>> Register(RegisterViewModel model)
        {
            var successRegister = this.userService.Register(model.Username, model.Password);

            if (!successRegister)
            {
                //TODO: when extended
            }

            return this.All();
        }

        [HttpGet]
        public IActionResult<IEnumerable<UserViewModel>> All()
        {
            var allUsers = this.userService.All();

            return View(allUsers);
        }

        [HttpGet]
        public IActionResult<UserViewModel> Profile(int id)
        {
            var model = this.userService.GetById(id);

            return View(model);
        }

        [HttpPost]
        public IActionResult<UserViewModel> Profile(NoteCreateModel model)
        {
            this.noteService.Add(model.Title, model.Content, model.UserId);

            return this.Profile(model.UserId);
        }
    }
}