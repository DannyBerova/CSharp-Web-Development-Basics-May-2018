namespace ExamWeb.App.Infrastructure
{
    public static class Constants
    {
        public const string UsersTopMenuHtml = @"
                <li class=""navbar-nav"">
                    <form action=""/home/index"" method=""get"" class=""form-inline my-2 my-lg-0"">
                        <input class=""form-control mr-sm-2"" type=""text"" name=""search"" placeholder=""Search"" aria-label=""Search"">
                        <button class=""btn btn-dark mr-3 my-2 my-sm-0"" type=""submit"">Search</button>
                    </form>
                </li>
                <li class=""nav-item active"">
	                <a class=""nav-link"" href=""/posts/add"">New Post</a>
                </li>
                <li class=""nav-item active"">
	                <a class=""nav-link"" href=""/users/logout"">Logout</a>
                </li>";

        public const string GuestsTopMenuHtml = @"
                <li class=""nav-item active"">
	                <a class=""nav-link"" href=""/home/index"">Home</a>
                </li>
                <li class=""nav-item active"">
	                <a class=""nav-link"" href=""/users/login"">Login</a>
                </li>
                <li class=""nav-item active"">
	                <a class=""nav-link"" href=""/users/register"">Register</a>
                </li>";

        public const string AdminOnlyMenu = @"
                <li class=""nav-item dropdown float-left"">
                     <a class=""nav-link dropdown-toggle"" data-toggle=""dropdown"" href=""#"" role=""button"" aria-haspopup=""true"" aria-expanded=""false"">Admin</a>
                     <div class=""dropdown-menu"">
                          <a class=""dropdown-item"" href=""/admin/log"">Log</a>
                          <a class=""dropdown-item"" href=""/admin/posts"">Posts</a>
                          <a class=""dropdown-item"" href=""/admin/users"">Users</a>
                    </div>
               </li>";

        public const string UserFormatPath = "../../../Views/Home/User.html";

        public const string AdminFormatPath = "../../../Views/Home/Admin.html";

        public const string GuestFormatPath = "../../../Views/Home/Guest.html";

        public const string InvalidPossitionMessage = "Invalid Position!";

        public const string UnsuccessfullRegistrationMessage = "Unsuccessfull Registration!";

        public const string UnsuccessfullOperationMessage = "Unsuccessfull Operation!";

        public const string InvalidCredentialsMessage = "Invalid Credentials";
    }
}
