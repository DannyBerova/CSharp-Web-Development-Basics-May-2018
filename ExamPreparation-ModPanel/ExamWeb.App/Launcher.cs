namespace ExamWeb.App
{
    using Data;
    using SimpleMvc.Framework;
    using SimpleMvc.Framework.Routers;
    using WebServer;

    public class Launcher
    {
       public static void Main()
        {
            var context = new ModPanelDbContext();

            var server = new WebServer(
                42420,
                new ControllerRouter(),
                new ResourceRouter());
            MvcEngine.Run(server, new ModPanelDbContext());
        }
    }
}
