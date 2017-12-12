using Microsoft.AspNetCore.Mvc.Filters;

namespace Main.Attributes
{
    public class ApiExceptionFilter : IExceptionFilter
    {
        #region Methods

        /// <summary>
        /// Callback which is fired when exception occured.
        /// </summary>
        /// <param name="context"></param>
        public void OnException(ExceptionContext context)
        {
            var a = context;
        }

        #endregion
    }
}