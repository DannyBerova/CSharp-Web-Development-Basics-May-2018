namespace SimpleMvc.App.Models
{
    using System.Collections.Generic;
    using SimpleMvc.Models;

    public class UserViewModel
    {
        public int Id { get; set; }

        public string Username { get; set; }

        public IEnumerable<Note> Notes { get; set; }
    }
}