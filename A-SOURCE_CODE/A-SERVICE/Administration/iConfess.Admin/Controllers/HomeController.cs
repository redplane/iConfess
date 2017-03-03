using System.Web.Mvc;

namespace iConfess.Admin.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public ActionResult Index()
        {
            return View();
        }
    }
}