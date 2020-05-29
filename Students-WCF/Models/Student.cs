using System.Runtime.Serialization; 

namespace StudentsService.Models
{
    [DataContract]
    public class Student
    {
        [DataMember]
        public string Name { set; get; }

        [DataMember]
        public string Surname { set; get; }

        [DataMember]
        public float AvgMark { set; get; }
    }
}