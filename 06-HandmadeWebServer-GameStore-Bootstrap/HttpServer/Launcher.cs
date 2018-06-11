namespace HttpServer
{
    using ByTheCakeApplication;
    using GameStoreApplication;

    using Server;
    using Server.Contracts;
    using Server.Routing;

    public class Launcher : IRunnable
    {
        //credits for architecture and functionality to 
        //ruskovweb@GitHub
        //msotiroff@GitHub

        public static void Main()
        {
            new Launcher().Run();
        }

        public void Run()
        {
            var mainApplication = new GameStoreApp();
            var appRouteConfig = new AppRouteConfig();
            mainApplication.InitalizeDatabase();
            mainApplication.Configure(appRouteConfig);

            var webServer = new WebServer(8230, appRouteConfig);
            webServer.Run();
        }
    }
}
