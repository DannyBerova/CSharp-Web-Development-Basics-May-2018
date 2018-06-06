using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace MyWebServer.Server.Http
{
    public static class SessionStore
    {
        public const string sessionCookieKey = "MY_SID";

        private static readonly ConcurrentDictionary<string, HttpSession> sessions = 
            new ConcurrentDictionary<string, HttpSession>();

        public static HttpSession Get(string id)
        => sessions.GetOrAdd(id, _ =>  new HttpSession(id));
       
    }
}
