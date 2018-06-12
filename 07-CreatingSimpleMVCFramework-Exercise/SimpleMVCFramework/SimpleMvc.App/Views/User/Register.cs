namespace SimpleMvc.App.Views.User
{
    using System.IO;
    using SimpleMVC.Framework.Interfaces;

    public class Register : IRenderable
    {
        public string Render()
        {
            var page = File.ReadAllText(@"..\..\..\ViewFiles\User\Register.html");

            return page;
        }
    }
}