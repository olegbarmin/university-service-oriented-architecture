using System;
using System.Web.Mvc;

namespace Task4.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudentsService.StudentsServiceSoapClient client = new StudentsService.StudentsServiceSoapClient();

        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult getAllStudents()
        {
            var studentsArr = client.GetAllStudents();
            return View("Result", studentsArr);
        }

        [HttpPost]
        public ActionResult GetStudentsFilteredByAverageMark(string lowerBound, string upperBound)
        {
            var lowerBoundValue = Convert.ToInt32(lowerBound);
            var upperBoundValue = Convert.ToInt32(upperBound);

            var studentsArr = client.GetStudentsFilteredByAverageMark(lowerBoundValue, upperBoundValue);
            return View("Result", studentsArr);
        }
    }
}