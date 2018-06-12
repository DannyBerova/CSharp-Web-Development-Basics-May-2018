namespace SimpleMvc.App
{
    using Data;
    using Microsoft.EntityFrameworkCore;
    using SimpleMVC.Framework;
    using SimpleMVC.Framework.Routers;
    using WebServer;

    public class Launcher
    {
        static void Main()
        {
            using (var db = new NotesDbContext())
            {
              db.Database.Migrate();
            }

            var server = new WebServer(42420, new ControllerRouter());

            MvcEngine.Run(server);
        }
    }
}
