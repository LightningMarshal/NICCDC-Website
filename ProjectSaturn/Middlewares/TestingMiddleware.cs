using System.Diagnostics;
using ProjectSaturn.Models;

namespace ProjectSaturn.Middlewares
{
    public class TestingMiddleware
    {
        private readonly RequestDelegate _next;

        public TestingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            if (ErrorLog.Msglist.Count > 0)
            {
                Debug.WriteLine("---------- Errors Detected ----------");
            }
            int num = 1;
            foreach (string msg in ErrorLog.Msglist)
            {
                Debug.WriteLine("Error " + num + ":" + msg);
            }

            return _next(httpContext);
        }
    }
}
