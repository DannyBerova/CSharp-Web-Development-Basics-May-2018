namespace SimpleMvc.Services.ViewModels
{
    using System.Collections.Generic;
    using Modelss;

    public class UserViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public ICollection<Tube> Tubes { get; set; }
    }
}