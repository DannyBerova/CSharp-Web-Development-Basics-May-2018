namespace MyWebServer.ByTheCakeApp.ViewModels.Account
{
    using System;

    public class ProfileViewModel
    {
        public string Name { get; set; }

        public DateTime RegistrationDate { get; set; }

        public int TotalOrders { get; set; }
    }
}
