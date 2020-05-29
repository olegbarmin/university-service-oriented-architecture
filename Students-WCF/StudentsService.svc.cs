using StudentsService.Models;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Xml.Serialization;

namespace StudentsService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "StudentsServiceImpl" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select StudentsServiceImpl.svc or StudentsServiceImpl.svc.cs at the Solution Explorer and start debugging.
    public class StudentsServiceImpl : IStudentsService
    {
        const string PATH_TO_STUDENTS_XML_FILE = "~/friends.xml";

        public Student[] GetAllStudents()
        {
            return LoadStudentsFromFile();
        }

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
