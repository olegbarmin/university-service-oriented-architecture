using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using WpfAppForTask5.Helpers;
using WpfAppForTask5.StudentsService;

namespace WpfAppForTask5
{
    public class MainWindowViewModel : Observable
    {
        private readonly StudentsService.StudentsService service = new StudentsService.StudentsService();

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

        private void LowerSubmitClick()
        {
            var temp = service.GetAllStudents();
            populateTable(temp);
        }

        private void InRangeSubmitClick()
        {
            float lowerBound;
            float upperBound;
            if (float.TryParse(lowerBoundInput, out lowerBound) && float.TryParse(upperBoundInput, out upperBound))
            {
                var studentsArr = service.GetStudentsFilteredByAverageMark(lowerBound, upperBound);
                populateTable(studentsArr);
            }
        }

        private void populateTable(Student[] students)
        {
            StudentsTable.Clear();
            Array.ForEach(students, s => StudentsTable.Add(s));
        }
    }
}
