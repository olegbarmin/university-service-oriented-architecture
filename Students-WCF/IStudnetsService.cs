using StudentsService.Models;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace StudentsService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IStudentsService" in both code and config file together.
    [ServiceContract(Namespace = "http://friends.com/wcf")]
    public interface IStudentsService
    {

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped,
           RequestFormat = WebMessageFormat.Json,
           ResponseFormat = WebMessageFormat.Json,
           UriTemplate = "GetAllStudents",
           Method = "POST")]
        Student[] GetAllStudents();

        [OperationContract]
        [WebInvoke(BodyStyle = WebMessageBodyStyle.Wrapped,
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            UriTemplate = "GetStudentsFilteredByAverageMark",
            Method = "POST")]
        Student[] GetStudentsFilteredByAverageMark(float lowerBound, float upperBound);
    }
}
