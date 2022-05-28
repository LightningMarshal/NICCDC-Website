using ProjectSaturn.Models;
using System.Diagnostics;

namespace ProjectSaturn.Middlewares
{
    public class CookieMiddleware // Ensures that a user is present before allowing to Application Pages
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
                if (destination == "/Application/PersonalDetails" || destination == "/Application/GeneralDetails" || destination == "/Application/EducationDetails" || destination == "/Application/ProfessionalDetails" || destination == "/Application/CertificationDetails" || destination == "/Application/SkillsDetails" || destination == "/Application/AwardsDetails")
                {
                    ErrorLog.Msglist.Add("Redirected: " + destination + " - > " + "/Application/Home");
                    httpContext.Response.Redirect("/Application/Home");
                }
            }

            return _next(httpContext);
        }
    }
}
