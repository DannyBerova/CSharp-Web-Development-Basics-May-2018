using System;
using System.Collections.Generic;

namespace Task3RequestParser
{
    class RequestParser
    {
        static void Main()
        {
            var validUrls = new Dictionary<string, HashSet<string>>();

            string input;
            while ((input = Console.ReadLine()) != "END" )
            {
                var urlParts = input.Split('/', StringSplitOptions.RemoveEmptyEntries);
                var path = $"/{urlParts[0]}";
                var method = urlParts[1];

                if (!validUrls.ContainsKey(path))
                {
                    validUrls[path] = new HashSet<string>();
                }

                validUrls[path].Add(method);
            }

            var request = Console.ReadLine();
            var reqParts = request.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var reqMethod = reqParts[0];
            var reqUrl = reqParts[1];
            var reqProtocol = reqParts[2];

            var responseCode = 404;
            var responseStatusText = "Not Found";

            if (validUrls.ContainsKey(reqUrl) && validUrls[reqUrl].Contains(reqMethod.ToLower()))
            {
                responseCode = 200;
                responseStatusText = "OK";
            }

            Console.WriteLine($"{reqProtocol} {responseCode} {responseStatusText}");
            Console.WriteLine($"Content-Length: {responseStatusText.Length}");
            Console.WriteLine($"Content-Type-text/plain" + Environment.NewLine);
            Console.WriteLine(responseStatusText);
        }
    }
}
