using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using MyWebServer.Server.Common;
using MyWebServer.Server.Handlers;
using MyWebServer.Server.Http;
using MyWebServer.Server.Http.Contracts;
using MyWebServer.Server.Routing.Contracts;

namespace MyWebServer.Server
{
    public class ConnectionHandler
    {
        private readonly Socket client;

        private readonly IServerRouteConfig serverRouteConfig;

        public ConnectionHandler(Socket client, IServerRouteConfig serverRouteConfig)
        {
            CoreValidator.ThrowIfNull(client, nameof(client));
            CoreValidator.ThrowIfNull(serverRouteConfig, nameof(serverRouteConfig));

            this.client = client;
            this.serverRouteConfig = serverRouteConfig;
        }

        public async Task ProcessRequestAsync()
        {
            var httpRequest = await this.ReadRequest();

            if (httpRequest != null)
            {
                var httpContext = new HttpContext(httpRequest);

                var httpResponse = new HttpHandler(this.serverRouteConfig).Handle(httpContext);

                var responseBytes = Encoding.UTF8.GetBytes(httpResponse.ToString());

                var byteSegment = new ArraySegment<byte>(responseBytes);

                await this.client.SendAsync(byteSegment, SocketFlags.None);

                Console.WriteLine($"-----REQUEST-----");
                Console.WriteLine(httpRequest);
                Console.WriteLine($"-----RESPONSE-----");
                Console.WriteLine(httpResponse);
                Console.WriteLine();
            }

            this.client.Shutdown(SocketShutdown.Both);
        }

        private async Task<IHttpRequest> ReadRequest()
        {
            var result = new StringBuilder();

            var data = new ArraySegment<byte>(new byte[1024]);

            //while ((numBytesRead = await this.client.ReceiveAsync(data, SocketFlags.None)) > 0)
            //{
            //    request = Encoding.ASCII.GetString(data.Array, 0, numBytesRead);
            //    if (numBytesRead < 1023) break;
            //}

            while (true)
            {
                int numberOfBytesRead = await this.client.ReceiveAsync(data, SocketFlags.None);

                if (numberOfBytesRead == 0)
                {
                    break;
                }

                var bytesAsString = Encoding.UTF8.GetString(data.Array, 0, numberOfBytesRead);

                result.Append(bytesAsString);

                if (numberOfBytesRead < 1023)
                {
                    break;
                }
            }

            if (result.Length == 0)
            {
                return null;
            }

            return new HttpRequest(result.ToString());
        }
    }
}
