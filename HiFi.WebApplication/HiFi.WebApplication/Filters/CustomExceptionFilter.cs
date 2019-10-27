using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace HiFi.WebApplication.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private ILogger _logger;
        public CustomExceptionFilter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CustomExceptionFilter>();
        }
        public void OnException(ExceptionContext context)
        {
            string controllername = (string)context.RouteData.Values["Controller"];
            string actionName = (string)context.RouteData.Values["Action"];
            _logger.LogError("OnException : " + actionName + " Action invoked from " + controllername + " Controller");

            HttpStatusCode status = HttpStatusCode.InternalServerError;
            string message = "Server error occurred.";
             
            message = context.Exception.Message;
            //You can enable logging error

            context.ExceptionHandled = true;
            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            response.ContentType = "application/json";
            context.Result = new ViewResult()
            {
                ViewName = "Error"
            };
        }
    }
}
