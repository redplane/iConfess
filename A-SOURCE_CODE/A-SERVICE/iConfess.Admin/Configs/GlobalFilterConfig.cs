using System.Web.Mvc;
using iConfess.Admin.Middlewares;

namespace iConfess.Admin
{
    public class GlobalFilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection globalFilterCollection)
        {
            // Error handler registration.
            globalFilterCollection.Add(new HandleErrorAttribute());

            // Authentication middleware registration.
            globalFilterCollection.Add(new BearerAuthenticationMiddleware());
        }
    }
}