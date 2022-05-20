using ProjectSaturn.Models;
using System.Diagnostics;

namespace ProjectSaturn.Middlewares
{
    public class CookieMiddleware // Ensures that a user is present before allowing to Creator Pages
    {
        private readonly RequestDelegate _next;

        public CookieMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var user = httpContext.Request.Cookies["user"];

            var destination = httpContext.Request.Path.Value;
            if (user == null)
            {
                // Redirect if user does not have a User ID
                if (destination == "/Creator/PersonalDetails" || destination == "/Creator/GeneralDetails" || destination == "/Creator/EducationDetails" || destination == "/Creator/ProfessionalDetails" || destination == "/Creator/CertificationDetails" || destination == "/Creator/SkillsDetails" || destination == "/Creator/AwardsDetails")
                {
                    ErrorLog.Msglist.Add("Redirected: " + destination + " - > " + "/Creator/Home");
                    httpContext.Response.Redirect("/Creator/Home");
                }
            }

            return _next(httpContext);
        }
    }
}
