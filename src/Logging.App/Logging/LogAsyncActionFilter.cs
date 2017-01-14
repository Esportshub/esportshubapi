using System;
using System.Threading.Tasks;

namespace Logging.App.Logging
{
    public class LogAsyncActionFilter : IAsyncActionFilter
    {
        public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controller = context.RouteData.Values["Controller"];
            var action = context.RouteData.Values["Action"];
            Console.WriteLine("Request received, Controller: {0}  at Action: {1}", controller, action);
            return next.Invoke();
        }
    }
}