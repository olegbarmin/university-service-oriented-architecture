using System.Linq;
using System.Web.Services;
using System.Web.Hosting;
using System.Xml.Serialization;
using System.IO;
using Students.Models;

namespace Students
{

    [WebService(Name = "StudentsService", Namespace = "http://www.friends.com/")]
    public class StudentsService : WebService
    {
        const string PATH_TO_STUDENTS_XML_FILE = "~/friends.xml";

        [WebMethod]
        public Student[] GetAllStudents()
        {
            return LoadStudentsFromFile();
        }

        [WebMethod]
        public Student[] GetStudentsFilteredByAverageMark(float lowerBound, float upperBound)
        {
            var students = LoadStudentsFromFile();
            return students.Where(s => s.AvgMark >= lowerBound && s.AvgMark <= upperBound).ToArray();
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
