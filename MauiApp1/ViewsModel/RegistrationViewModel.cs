using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Model;
using MauiApp1.Services;
using System.Threading.Tasks;

namespace MauiApp1.ViewModel
{
    public partial class RegistrationViewModel : ObservableObject
    {
        private readonly StudentService _studentService;

        [ObservableProperty]
        string name = "";

        [ObservableProperty]
        string email = "";

        [ObservableProperty]
        string password = "";

        [ObservableProperty]
        string faculty = "";

        [ObservableProperty]
        string major = "";

        [ObservableProperty]
        string year = ""; // เพิ่ม property สำหรับปีการศึกษา

        public RegistrationViewModel(StudentService studentService)
        {
            _studentService = studentService;
        }

        private string GenerateRandomId()
        {
            var random = new Random();
            return random.Next(10000, 99999).ToString();
        }

        [RelayCommand]
        async Task Register()
        {
            // ตรวจสอบข้อมูล
            if (string.IsNullOrEmpty(Name) || 
                string.IsNullOrEmpty(Email) || 
                string.IsNullOrEmpty(Password) ||
                string.IsNullOrEmpty(Faculty) ||
                string.IsNullOrEmpty(Major) ||
                string.IsNullOrEmpty(Year))
            {
                await Shell.Current.DisplayAlert("Error", "Please fill in all fields.", "OK");
                return;
            }

            // ตรวจสอบว่า Year เป็นตัวเลข
            if (!int.TryParse(Year, out int yearValue) || yearValue <= 0)
            {
                await Shell.Current.DisplayAlert("Error", "Please enter a valid year (number).", "OK");
                return;
            }

            // สร้างนักเรียนใหม่
            var newStudent = new Student
            {
                Id = GenerateRandomId(),
                Email = Email,
                Password = Password,
                Profile = new Profile
                {
                    Name = Name,
                    Faculty = Faculty,
                    Major = Major,
                    Year = yearValue // ใช้ค่าปีการศึกษาที่ผู้ใช้กรอก
                },
                CurrentTerm = new Term
                {
                    TermTerm = "2/2568", // ตั้งค่าเทอมปัจจุบันเป็น 2/2568
                    EnrolledCourses = new List<string>()
                },
                PreviousTerms = new List<Term>()
            };

            // บันทึกข้อมูลนักเรียนใหม่
            var result = await _studentService.AddStudentAsync(newStudent);

            if (result)
            {
                await Shell.Current.DisplayAlert("Success", "Registration successful!", "OK");
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "Registration failed. Email may already exist.", "OK");
            }
        }
    }
}