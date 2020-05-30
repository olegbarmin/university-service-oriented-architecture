using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using StudentsClient.Helpers;
using StudentsClient.Models;

namespace StudentsClient
{
    public class MainWindowViewModel : Observable
    {


        private ICommand showAllStudentsCmd;
        private ICommand showFilteredStudentsCmd;

        private string getStudentsInRangeMin = "1";
        private string getStudentsInRangeMax = "5";

        public ObservableCollection<Student> StudentsTable { get; } = new ObservableCollection<Student>();

        public string lowerBoundInput
        {
            get { return getStudentsInRangeMin; }
            set { Set(ref getStudentsInRangeMin, value); }
        }
        public string upperBoundInput
        {
            get { return getStudentsInRangeMax; }
            set { Set(ref getStudentsInRangeMax, value); }
        }

        public ICommand showAllStudentClick => showAllStudentsCmd ?? (showAllStudentsCmd = new RelayCommand(LowerSubmitClick));

        public ICommand filteredStudentsClick => showFilteredStudentsCmd ?? (showFilteredStudentsCmd = new RelayCommand(InRangeSubmitClick));

        private async void LowerSubmitClick()
        {
            var temp = await StudentsApi.GetAllStudents();
            populateTable(temp);
        }

        private async void InRangeSubmitClick()
        {
            float lowerBound;
            float upperBound;
            if (Single.TryParse(getStudentsInRangeMin, out lowerBound) && Single.TryParse(getStudentsInRangeMax, out upperBound))
            {
                var students = await StudentsApi.GetStudentsFilteredByAverageMark(lowerBound, upperBound);
                populateTable(students);
            }
        }

        private void populateTable(Student[] students)
        {
            StudentsTable.Clear();
            Array.ForEach(students, s => StudentsTable.Add(s));
        }
    }
}
