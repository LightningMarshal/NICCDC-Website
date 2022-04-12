using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ProjectSaturn.Service;

namespace ProjectSaturn.Middlewares
{
    public class CookieMiddleware
    {
        private readonly RequestDelegate _next;

        public CookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext, DAL _dal)
        {
            var user = httpContext.Request.Cookies["user"];

            var option = new CookieOptions
            {
                Expires = new DateTimeOffset(DateTime.Now.AddDays(1))
            };

            if (user == null)
            {
                Guid guid = _dal.AddTempUser();// Make a temporary user in DAL

                string strguid = guid.ToString();

                httpContext.Response.Cookies.Append("user", strguid, option);
            }

            return _next(httpContext);
        }
    }
}
