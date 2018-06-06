using System;
using System.Net;
using System.Text;

namespace Task2ValidateURL
{
    class ValidateURL
    {
        static void Main()
        {
            try
            {
                var inputUri = Console.ReadLine();
                var uriTest = new Uri(inputUri);

                var result = new StringBuilder();

                if (!uriTest.IsWellFormedOriginalString())
                {
                    result.AppendLine("Invalid URL");
                }

                var validHttp = uriTest.Port == 80 && uriTest.Scheme == "http";
                var validHttps = uriTest.Port == 443 && uriTest.Scheme == "https";

                if (validHttps || validHttp)
                {
                    result.AppendLine($"Protocol: {uriTest.Scheme}");
                    result.AppendLine($"Host: {uriTest.DnsSafeHost}");
                    result.AppendLine($"Port: {uriTest.Port}");
                    var path = WebUtility.UrlDecode(uriTest.LocalPath);
                    result.AppendLine($"Path: {path}");

                    var query = WebUtility.UrlDecode(uriTest.Query);
                    if (uriTest.Query != string.Empty)
                    {
                        result.AppendLine($"Query: {query.TrimStart('?')}");
                    }

                    var fragment = WebUtility.UrlDecode(uriTest.Fragment);
                    if (uriTest.Fragment != string.Empty)
                    {
                        result.AppendLine($"Fragment: {fragment.TrimStart('#')}");
                    }
                }
                else
                {
                    result.AppendLine("Invalid URL");
                }

                Console.WriteLine(result.ToString().Trim());
            }
            catch (Exception e)
            {
                Console.WriteLine("Invalid URL");
            }
        }
    }
}
