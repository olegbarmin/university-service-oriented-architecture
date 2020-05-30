using System.Web.Mvc;

namespace StudentsWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Students Web Service";
            return View();
        }
    }
}
