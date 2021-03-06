﻿namespace MyWebServer.Server
{
    using Contracts;
    using Routing;
    using Routing.Contracts;
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading.Tasks;

    public class WebServer : IRunnable
    {
        private const string localHostIpAddress = "127.0.0.1";

        private readonly int port;
        private readonly IServerRouteConfig serverRouteConfig;
        private readonly TcpListener tcpListener;
        private bool isRunning;

        public WebServer(int port, IAppRouteConfig routeConfig)
        {
            this.port = port;
            this.tcpListener = new TcpListener(IPAddress.Parse(localHostIpAddress), port);

            this.serverRouteConfig = new ServerRouteConfig(routeConfig);
        }
        public void Run()
        {
            this.tcpListener.Start();
            this.isRunning = true;

            Console.WriteLine($"Server started. Listening to TCP clients at {localHostIpAddress}:{this.port}");

            //Task task = Task.Run(this.ListenLoop);
            //task.Wait();

            //try to catch exception without crashing the server when testing
            //don't know if it's necessary

            //try
            //{
                Task.Run(ListenLoop).Wait();
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
                
            //}
        }

        private async Task ListenLoop()
        {
            while (this.isRunning)
            {
                Socket client = await this.tcpListener.AcceptSocketAsync();
                ConnectionHandler connectionHandler = new ConnectionHandler(client, this.serverRouteConfig);
                //Task connection = connectionHandler.ProcessRequestAsync();
                //connection.Wait();
                await connectionHandler.ProcessRequestAsync();
            }
        }
    }
}
