using System;

namespace Students.Models
{
    [Serializable]
    public class Student
    {
        public string Name { set; get; }

        public string Surname { set; get; }

        public float AvgMark { set;  get; }
    }
}