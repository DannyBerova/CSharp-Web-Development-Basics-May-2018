namespace MyWebServer
{
    using System;
    using ByTheCakeApp;
    using Microsoft.EntityFrameworkCore.Migrations.Operations;
    using Server;
    using Server.Contracts;
    using Server.Routing;

    public class Launcher : IRunnable
   {
       //private WebServer webServer;

       public static void Main()
        {
            Console.WriteLine(@"Please, press ENTER if you already added 
InitialMigration with Package Manager Console 
and changed the connection string if needed...");

            Console.ReadLine();

            new Launcher().Run();
        }

       public void Run()
       {
           var mainApplication = new ByTheCakeApplication();
           mainApplication.InitializeDatabase();

            var routeConfig = new AppRouteConfig();
           mainApplication.Configure(routeConfig);

           var webServer = new WebServer(8230, routeConfig);

           webServer.Run();
       }
   }
}
