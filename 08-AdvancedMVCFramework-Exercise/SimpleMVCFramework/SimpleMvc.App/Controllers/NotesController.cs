namespace SimpleMvc.App.Controllers
{
    using BindingModels;
    using GameStore.App.Infrastructure;
    using Services;
    using SimpleMVC.Framework.Attributes.Methods;
    using SimpleMVC.Framework.Interfaces;
    using System.Linq;

    public class NotesController : BaseController
    {
        public const string CreateNoteErrorMessage = @"<p>Check your form for errors.</p><p> Title must be between 3 symbols minimum.</p><p>Content must be 4 symbols minimum.</p>";

        private readonly UserService userService;
        private readonly NoteService noteService;

        public NotesController()
        {
            this.userService = new UserService();
            this.noteService = new NoteService();
        }

        [HttpGet]
        public IActionResult Add()
        {
            if (!this.User.IsAuthenticated)
            {
                return RedirectToLogin();
            }

            this.Model["username"] = this.User.Name;
            return View();
        }


        [HttpPost]
        public IActionResult Add(NoteCreateModel model)
        {
            if (!this.User.IsAuthenticated)
            {
                RedirectToLogin();
            }
            if (!this.IsValidModel(model))
            {
                this.ShowError(CreateNoteErrorMessage);
                return RedirectToAction("/notes/add");
            }

            var username = this.User.Name;

            this.noteService.Add(model.Title.CapitalizeFirstLetter(), model.Content, username);

            var user = this.userService.GetByName(username);

            this.Model["username"] = user.Username;
            this.Model["notes"] =
                user.Notes.Any()
                    ? string.Join(string.Empty,
                        user.Notes.Select(n => $@"<li><strong>{n.Title}</strong> - {n.Content}</li>"))
                    : "No note records";

            return View();
        }

        [HttpGet]
        public IActionResult Info(int id)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            var note = this.noteService.GetById(id);

            if (note == null)
            {
                this.ShowError("Note not found!");
                return View();
            }

            this.Model["username"] = this.User.Name;
            this.Model["id"] = id.ToString();
            this.Model["title"] = note.Title;
            this.Model["content"] = note.Content;
            this.Model["isfinished"] = note.IsFinished ? "Complete" : "In Progres";

            return this.View();
        }

        [HttpPost]
        public IActionResult Check(int id)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            var note = this.noteService.GetById(id);
            bool isCheked = note.IsFinished;

            this.noteService.ChangeStatus(id, isCheked);

            var user = this.userService.GetByName(this.User.Name);

            return RedirectToAction($"/notes/info?id={note.Id}");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            var note = this.noteService.GetById(id);

            if (note == null)
            {
                this.ShowError("Note not found!");
                return View();
            }

            this.Model["username"] = this.User.Name;
            this.Model["id"] = id.ToString();
            this.Model["title"] = note.Title.CapitalizeFirstLetter();
            this.Model["content"] = note.Content;

            return this.View();
        }

        [HttpPost]
        public IActionResult Edit(int id, NoteEditModel model)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            if (!this.IsValidModel(model))
            {
                this.ShowError(CreateNoteErrorMessage);
                return this.View();
            }

            this.noteService.Update(
                id,
                model.Title,
                model.Content);

            var user = this.userService.GetByName(this.User.Name);

            return RedirectToAction($"/user/profile?id={user.Id}");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            var note = this.noteService.GetById(id);

            if (note == null)
            {
                this.ShowError("Note not found!");
                return View();
            }

            this.Model["username"] = this.User.Name;
            this.Model["id"] = id.ToString();
            this.Model["title"] = note.Title;
            this.Model["content"] = note.Content;

            return this.View();
        }

        [HttpPost]
        public IActionResult Destroy(int id)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.RedirectToHome();
            }

            var note = this.noteService.GetById(id);

            if (note == null)
            {
                this.ShowError("Note not found!");
                return View();
            }

            this.noteService.Delete(id);
            var user = this.userService.GetByName(this.User.Name);

            return RedirectToAction($"/user/profile?id={user.Id}");
        }
    }
}
