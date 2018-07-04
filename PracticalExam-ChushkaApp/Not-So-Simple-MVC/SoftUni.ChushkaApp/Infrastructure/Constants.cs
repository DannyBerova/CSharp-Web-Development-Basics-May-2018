namespace SoftUni.ChushkaApp.Infrastructure
{
    public static class Constants
    {
        public const string UsersTopMenuHtml = @"
                <ul class=""navbar-nav right-side"">
                    <li class=""nav-item"">
                        <a class=""nav-link nav-link-white"" href=""/"">Home</a>
                    </li>
                </ul>
                <ul class=""navbar-nav left-side"">
                    <li class=""nav-item"">
                        <a class=""nav-link nav-link-white float-right"" href=""/users/logout"">Logout</a>
                    </li>
                </ul>";

        public const string GuestsTopMenuHtml = @"
                <ul class=""navbar-nav"">
                    <li class=""nav-item"">
                        <a class=""nav-link nav-link-white"" href=""/home/index"">Home</a>
                    </li>
                    <li class=""nav-item"">
                        <a class=""nav-link nav-link-white"" href=""/users/login"">Login</a>
                    </li>
                    <li class=""nav-item"">
                        <a class=""nav-link nav-link-white"" href=""/users/register"">Register</a>
                    </li>
                </ul>";

        public const string AdminOnlyMenu = @"
<ul class=""navbar-nav right-side"">
                <li class=""nav-item active"">
	                <a class=""nav-link"" href=""/home/index"">Home</a>
                </li>
                <li class=""nav-item active"">
	                <a class=""nav-link"" href=""/products/add"">Create Product</a>
                </li>
                <li class=""nav-item active"">
	                <a class=""nav-link"" href=""/admin/orders"">All Orders</a>
                </li>
</ul>
<ul class=""navbar-nav left-side"">
                <li class=""nav-item active float-right"">
	                <a class=""nav-link"" href=""/users/logout"">Logout</a>
                </li>
</ul>";

        public const string UserFormatPath = "../../../Views/Home/User.html";

        public const string AdminFormatPath = "../../../Views/Home/Admin.html";

        public const string GuestFormatPath = "../../../Views/Home/Guest.html";

        public const string InvalidProductTypeMessage = "Invalid Product Type!";

        public const string UnsuccessfullRegistrationMessage = "Unsuccessfull Registration!";

        public const string UnsuccessfullOperationMessage = "Unsuccessfull Operation!";

        public const string InvalidCredentialsMessage = "Invalid Credentials";

        public const string UserGreetingsMessage = "Feel free to view and order any of our products.";

        public const string AdminGreetingsMessage = "Enjoy your work today!";
    }
}
