using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Xml.Serialization;
using StudentsWeb.Models;

namespace StudentsWeb.Controllers
{
    public class StudentsController : ApiController
    {
        const string PATH_TO_STUDENTS_XML_FILE = "~/friends.xml";

        public StudentsController()
        {
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                HttpContext.Current.Response.End();
            }
        }

        [HttpGet]
        [Route("GetAllStudents")]
        public Student[] GetAllStudents()
        {
            return LoadStudentsFromFile();
        }

        [HttpPost]
        [Route("GetStudentsFilteredByAverageMark")]
        public Student[] GetStudentsFilteredByAverageMark([FromBody] (float lowerBound, float upperBound) boundsTuple)
        {
            var students = LoadStudentsFromFile();
            return students.Where(s => s.AvgMark >= boundsTuple.lowerBound && s.AvgMark <= boundsTuple.upperBound).ToArray();
        }

        private Student[] LoadStudentsFromFile()
        {
            var pathToFile = HostingEnvironment.MapPath(PATH_TO_STUDENTS_XML_FILE);
            var formatter = new XmlSerializer(typeof(Student[]));
            Student[] students;
            using (FileStream fs = new FileStream(pathToFile, FileMode.OpenOrCreate))
            {
                students = (Student[])formatter.Deserialize(fs);
            }
            return students.ToArray();
        }
    }
}
