namespace SimpleMvc.AppMeTube
{
    using Data;
    using Framework;
    using Framework.Routers;
    using WebServer;

    public class Launcher
    {
        public static void Main()
        {
            var server = new WebServer(42420, new ControllerRouter(), new ResourceRouter());

            MvcEngine.Run(server, new MeTubeDbContext());
        }
    }
}
