namespace SimpleMvc.App.Controllers
{
    using BindingModels;
    using GameStore.App.Infrastructure;
    using Services;
    using SimpleMVC.Framework.Attributes.Methods;
    using SimpleMVC.Framework.Interfaces;
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;

    public class UserController : BaseController
    {
        public const string RegisterErrorMessage = @"<p>Check your form for errors.</p><p> Username must be between 3 and 50 symbols.</p><p>Passwords must be between 4 and 30 symbols.</p><p>Confirm password must match the provided password.</p>";
        public const string ConfirmPasswordError = @"<p>Confirm password must match the provided password.</p>";
        public const string LoginErrorMessage = @"<p>Invalid User Credentials!</p>";
        public const string UnsuccessfulRegister = @"<p>Unsuccessful Register!</p>";

        private readonly UserService userService;
        private readonly NoteService noteService;

        public UserController()
        {
            this.userService = new UserService();
            this.noteService = new NoteService();
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (this.User.IsAuthenticated)
            {
                return RedirectToHome();
            }

            return View();
        }


        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (this.User.IsAuthenticated)
            {
                return RedirectToHome();
            }
            if (!IsValidModel(model))
            {
                ShowError(RegisterErrorMessage);
                return View();
            }
            if (model.Password != model.ConfirmPassword)
            {
                ShowError(ConfirmPasswordError);
                return View();
            }
            var successRegister = this.userService.Create(model.Username, model.Password);

            if (!successRegister)
            {
                ShowError(UnsuccessfulRegister);
                return View();
            }

            SignIn(model.Username);
            return RedirectToHome();
        }


        [HttpGet]
        public IActionResult  Login()
        {
            if (this.User.IsAuthenticated)
            {
                return RedirectToHome();
            }
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (this.User.IsAuthenticated)
            {
                return RedirectToHome();
            }
            if (!IsValidModel(model))
            {
                ShowError(LoginErrorMessage);
                return View();
            }

            var userExist = this.userService.UserExists(model.Username, model.Password);
            if (!userExist)
            {
                ShowError(LoginErrorMessage);
                return View();
            }

            SignIn(model.Username);
            return RedirectToHome();
        }

        [HttpGet]
        public IActionResult All()
        {
            if (!this.User.IsAuthenticated)
            {
                return RedirectToLogin();
            }

            var users = this.userService.All();

            this.Model["users"] =
                users.Any()
                    ? string.Join(string.Empty,
                        users.Select(u => $"<li><a href=\"/User/Profile?id={u.Id}\">{u.Username}</a></li>"))
                    : string.Empty;

            return View();
        }

        [HttpGet]
        public IActionResult Profile(int id)
        {
            if (!this.User.IsAuthenticated || this.User.Name != this.userService.GetById(id).Username)
            {
                return RedirectToHome();
            }

            return RedirectToAction("/user/list?page=1");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            SignOut();

            return RedirectToAction("/home/index");
        }

        [HttpGet]
        public IActionResult List(int page)
        {
            if (!this.User.IsAuthenticated )
            {
                return RedirectToAction("/home/index");
            }

            var user = this.userService.GetByName(this.User.Name);

            if (user == null || user.Username != this.User.Name)
            {
                return RedirectToAction("/home/index");
            }

            var noteFormat = File.ReadAllText("../../../Views/Notes/note-simple.html");


            const int notesPerPage = 4;
            int notesCount = this.noteService
                .GetNotesCountByUserId(user.Id);

            NoteViewModel[] notes = GetNotesByPage(page, notesPerPage, notesCount);
            GeneratePaginationView(page, notesPerPage, notesCount);


            this.Model["userId"] = user.Id.ToString();
            this.Model["username"] = user.Username;

            if (notes.Any())
            {
                this.Model["paginationDisplay"] = "block";
                this.Model["notes"] = "<div class=\"row\">" + string
                                .Join(Environment.NewLine, notes.Select(n =>
                                    string.Format(noteFormat,
                                    n.Title.Shortify(10),
                                    n.Content.Shortify(13),
                                    n.DateOfCreation.ToShortDateString(),
                                    n.IsFinished ? "Complete" : "In Progres",
                                    n.NoteId)
                                ))
                                + "</div>";
            }
            else
            {
                this.Model["paginationDisplay"] = "none";
                this.Model["notes"] = "<h3 style=\"text-align: center;\" class=\"text-danger\">You have not added any notes yet.</h3>";
            }

            return View();
        }

        private NoteViewModel[] GetNotesByPage(int page, int notesPerPage, int notesCount)
        {
            int skipNotes = (page - 1) * notesPerPage;

            if (skipNotes > notesCount)
            {
                return null;
            }

            NoteViewModel[] notes = this.noteService
                  .AllNotesFromUser(this.userService.GetByName(this.User.Name).Id)
                  .Skip(skipNotes)
                  .Take(notesPerPage)
                  .Select(n => new NoteViewModel()
                  {
                      Title = n.Title,
                      Content = n.Content.Substring(0, Math.Min(n.Content.Length, 100)),
                      DateOfCreation = n.CreationDate,
                      IsFinished = n.IsFinished,
                      NoteId = n.Id
                  })
                  .ToArray();

            return notes;
        }

        private void GeneratePaginationView(int page, int notesPerPage, int notesCount)
        {
            int totalPages = (int)Math.Ceiling((double)notesCount / notesPerPage);

            GeneratePrevNextButtons(page, notesPerPage, notesCount, totalPages);

            const int maxNumberOfPages = 3;
            int numberOfButtons = Math.Min(totalPages, maxNumberOfPages);

            GenerateMiddleButtnos(numberOfButtons, page, totalPages);
        }

        private void GenerateMiddleButtnos(int numberOfButtons, int page, int totalPages)
        {
            var builder = new StringBuilder();

            int startPage = page == 1 ? page : page - 1;

            if (numberOfButtons == 3 && page == totalPages)
            {
                startPage = page - 2;
            }

            for (int i = 0; i < numberOfButtons; i++)
            {
                if (startPage == page)
                {
                    builder.AppendLine($"<li class=\"page-item active\"><span class=\"page-link\">{startPage}<span class=\"sr-only\"(current)</span></span></li>");
                }
                else
                {
                    builder.AppendLine($"<li class=\"page-item\"><a class=\"page-link\" href=\"/user/list?page={startPage}\">{startPage}</a></li>");
                }
                startPage++;
            }

            this.Model["pages"] = builder.ToString();
        }

        private void GeneratePrevNextButtons(int page, int notesPerPage, int notesCount, int totalPages)
        {
            bool prevButtonIsActive = true;
            if (page == 1)
            {
                prevButtonIsActive = false;
            }

            this.Model["prevButton"] = GenerateButton(prevButtonIsActive, "Previous", page - 1);

            bool nextButtonIsActive = true;
            if (page == totalPages)
            {
                nextButtonIsActive = false;
            }

            this.Model["nextButton"] = GenerateButton(nextButtonIsActive, "Next", page + 1);
        }

        private string GenerateButton(bool isActive, string buttonName, int page)
        {
            string button = string.Empty;

            if (isActive)
            {
                button = $"<li class=\"page-item\"><a class=\"page-link\" href=\"/user/list?page={page}\">{buttonName}</a></li>";
            }
            else
            {
                button = $"<li class=\"page-item disabled\"><span class=\"page-link\">{buttonName}</span></li>";
            }

            return button;
        }
    }
}