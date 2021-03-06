﻿namespace MyWebServer
{
    using ByTheCakeApp;
    using Server;
    using Server.Contracts;
    using Server.Routing;

    public class Launcher : IRunnable
   {
       //private WebServer webServer;

       public static void Main()
        {
            new Launcher().Run();
        }

       public void Run()
       {
           var mainApplication = new ByTheCakeApplication();
           var routeConfig = new AppRouteConfig();
           mainApplication.Configure(routeConfig);

           var webServer = new WebServer(8230, routeConfig);

           webServer.Run();
       }
   }
}
