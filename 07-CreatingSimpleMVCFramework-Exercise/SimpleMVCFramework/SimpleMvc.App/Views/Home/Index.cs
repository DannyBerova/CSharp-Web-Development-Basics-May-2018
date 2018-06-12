namespace SimpleMvc.App.Views.Home
{
    using System.IO;
    using SimpleMVC.Framework.Interfaces;

    public class Index : IRenderable
    {
        public string Render()
        {
            return File.ReadAllText("../../../ViewFiles/Home/Index.html");
        }
    }
}
