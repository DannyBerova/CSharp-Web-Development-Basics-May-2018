using System;
using System.Collections.Generic;
using System.Text;

namespace MyWebServer.Server.Http.Contracts
{
    public interface IHttpSession
    {
        string Id { get; }

        object Get(string key);

        T Get<T>(string key);

        void Add(string key, object value);

        void Clear();
    }
}
