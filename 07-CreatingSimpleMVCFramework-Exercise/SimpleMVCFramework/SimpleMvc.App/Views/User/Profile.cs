namespace SimpleMvc.App.Views.User
{
    using System.Text;
    using Models;
    using SimpleMVC.Framework.Interfaces.Generic;

    public class Profile : IRenderable<UserViewModel>
    {
        public UserViewModel Model { get; set; }

        public string Render()
        {
            var builder = new StringBuilder();

            builder.AppendLine(@"<a href=""/user/all"">Back to All Users</a>");

            builder.AppendLine($"<h3>User: {Model.Username}</h3>");

            builder.AppendLine(@"<form action=""profile"" method=""post""");

            builder.AppendLine(@"<label for=""Title"">Title:</label>");
            builder.AppendLine(@"<input type=""text"" id=""Title"" name=""Title"" placeholder=""Enter Title"" required> <br />");
            builder.AppendLine(@"<label for=""Content"">Content:</label>");
            builder.AppendLine(@"<input type=""text"" id=""Content"" name=""Content"" placeholder=""Enter Content"" required> <br />");
            builder.AppendLine($@"<input type=""hidden"" name=""UserId"" value=""{Model.Id}"">");

            builder.AppendLine(@"<button type=""submit"">Add note</button> <br />");
            builder.AppendLine(@"</form>");

            builder.AppendLine(@"<h5>List of notes</h5>");

            builder.AppendLine("<ul>");

            foreach (var note in Model.Notes)
            {
                builder.AppendLine($@"<li><strong>{note.Title}</strong> - {note.Content}</li>");
            }

            builder.AppendLine("</ul>");

            return builder.ToString();
        }
    }
}