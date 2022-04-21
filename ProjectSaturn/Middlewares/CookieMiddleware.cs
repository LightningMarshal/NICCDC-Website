using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using ProjectSaturn.Models;
using ProjectSaturn.Service;
using System.Diagnostics;

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

            var destination = httpContext.Request.Path.Value;
            Debug.WriteLine("Destination: " + destination);

            if (user == null || user == Guid.Empty.ToString())
            {
                // Redirect if user does not have a User ID
                if (destination == "/Creator/PersonalDetails" || destination == "/Creator/EducationDetails" || destination == "/Creator/TrainingDetails" || destination == "/Creator/ProfessionalDetails" || destination == "/Creator/KnowledgeDetails" || destination == "/Creator/AwardsDetails")
                {
                    ErrorLog.Msglist.Add("Redirected: " + destination + " - > " + "/Creator/Home");
                    httpContext.Response.Redirect("/Creator/Home");
                }
            }

            return _next(httpContext);
        }
    }
}
