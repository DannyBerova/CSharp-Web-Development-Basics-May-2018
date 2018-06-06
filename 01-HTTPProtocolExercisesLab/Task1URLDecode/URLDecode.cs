using System;
using System.Net;

namespace Task1URLDecode
{
    class URLDecode
    {
        static void Main()
        {
            var inputLine = Console.ReadLine();
            var decodedUrl = WebUtility.UrlDecode(inputLine);
            Console.WriteLine(decodedUrl);
        }
    }
}
