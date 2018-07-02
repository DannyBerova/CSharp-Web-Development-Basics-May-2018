namespace SimpleMvc.AppMeTube.Infrastructure
{
    public static class Constants
    {
        public const string UsersTopMenuHtml = @"
                 <li class=""nav-item active col-md-3"">
                        <a class=""nav-link h5"" href=""/users/profile?id={{{id}}}"">Profile</a>
                    </li>
                    <li class=""nav-item active col-md-3"">
                        <a class=""nav-link h5"" href=""/tubes/add"">Upload</a>
                    </li>
                    <li class=""nav-item active col-md-3"">
                        <a class=""nav-link h5"" href=""/users/logout"">Logout</a>
                    </li>";

        public const string GuestsTopMenuHtml = @"
                 </li>
                    <li class=""nav-item active col-md-3"">
                        <a class=""nav-link h5"" href=""/users/login"">Login</a>
                    </li>
                    <li class=""nav-item active col-md-3"">
                        <a class=""nav-link h5"" href=""/users/register"">Register</a>
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

        public const string UserFormatPath = "../../../Views/Home/user.html";

        public const string GuestFormatPath = "../../../Views/Home/guest.html";

        public const string AdminFormatPath = "../../../Views/Home/Admin.html";

        public const string UnsuccessfullRegistrationMessage = "Unsuccessfull Registration!";

        public const string UnsuccessfullOperationMessage = "Unsuccessfull Operation!";

        public const string InvalidCredentialsMessage = "Invalid Credentials";

        public const string InvalidUrlMessage = "YouTube link must be valid YouTube Full URL.";
    }
}
