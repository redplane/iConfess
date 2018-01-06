using Microsoft.AspNetCore.Mvc.Filters;

namespace Main.Attributes
{
    public class ApiAuthenticationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext actionExecutingContext)
        {

        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}