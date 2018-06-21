namespace SimpleMvc.App.BindingModels
{
    using Models;
    using System.Collections.Generic;

    public class UserViewModel
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public IEnumerable<Note> Notes { get; set; }
    }
}