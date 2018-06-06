using System.Collections.Generic;

namespace MyWebServer.Server.Http.Contracts
{

    public interface IHttpHeaderCollection : IEnumerable<ICollection<HttpHeader>>
    {
        void Add(HttpHeader header);

        void Add(string key, string value);

        bool ContainsKey(string key);

        ICollection<HttpHeader> Get(string key);
    }
}
