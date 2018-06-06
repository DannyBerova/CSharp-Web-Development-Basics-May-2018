using System;
using MyWebServer.Application.Views.Home;
using MyWebServer.Server.Enums;
using MyWebServer.Server.Http;
using MyWebServer.Server.Http.Contracts;
using MyWebServer.Server.Http.Response;

namespace MyWebServer.Application.Controllers
{
    public class HomeController
    {
        //GET /
        public IHttpResponse Index()
        {
            var response = new ViewResponse(HttpStatusCode.Ok, new IndexView());

            response.Cookies.Add(new HttpCookie("lang", "en"));

            return response;
        }

        public IHttpResponse SessionTest(IHttpRequest req)
        {
            var session = req.Session;

            const string sessionDateKey = "saved_date";

            if (session.Get(sessionDateKey) == null)
            {
                session.Add(sessionDateKey, DateTime.UtcNow);
            }

            var response = new ViewResponse(
                HttpStatusCode.Ok,
                new SessionTestView(session.Get<DateTime>(sessionDateKey)));

            return response;
        }
    }
}
