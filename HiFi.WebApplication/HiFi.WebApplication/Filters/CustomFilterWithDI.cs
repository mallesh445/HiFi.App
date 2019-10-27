using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiFi.WebApplication.Filters
{
    public class CustomFilterWithDI : IActionFilter
    {
        private ILogger _logger;
        private string controllername;
        private string actionName;

        public CustomFilterWithDI(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<CustomFilterWithDI>();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //To do : before the action executes 
            controllername = (string)context.RouteData.Values["Controller"];
            actionName = (string)context.RouteData.Values["Action"];
            _logger.LogInformation("OnActionExecuted : " + actionName+" Action invoked from "+ controllername +" Controller");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //To do : after the action executes  
            controllername = (string)context.RouteData.Values["Controller"];
            actionName = (string)context.RouteData.Values["Action"];
            _logger.LogInformation("OnActionExecuting : " + actionName + " Action invoked from " + controllername + " Controller");
        }
    }
}
