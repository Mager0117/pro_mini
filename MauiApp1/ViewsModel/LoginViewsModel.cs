using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Model;
using MauiApp1.Page;
using MauiApp1.Services;

namespace MauiApp1.ViewsModel
{
    public partial class LoginViewsModel : ObservableObject
    {
        private readonly StudentService _studentService;

        [ObservableProperty]
        string email = "";

        [ObservableProperty]
        string password = "";

        // Inject StudentService ผ่าน constructor
        public LoginViewsModel(StudentService studentService)
        {
            _studentService = studentService;
        }

        [RelayCommand]
        async Task Login()
        {
            // อ่านข้อมูลนักศึกษาจาก StudentService
            var students = await _studentService.LoadStudentsAsync();

            // ค้นหานิสิตที่มีอีเมลและรหัสผ่านตรง
            var student = students.FirstOrDefault(s => s.Email == Email && s.Password == Password);

            if (student != null)
            {
                // ถ้าพบข้อมูลตรง ไปยังหน้า ShowObjectsPage พร้อมส่ง userId
                Debug.WriteLine("Login successful: " + student.Profile.Name);
                await GotoPage($"{nameof(ShowObjectsPage)}?userId={student.Id}");
            }
            else
            {
                // ถ้าไม่พบข้อมูล
                Debug.WriteLine("Invalid email or password.");
                await Shell.Current.DisplayAlert("Error", "Invalid email or password.", "OK");
            }
        }

        [RelayCommand]
        async Task GotoPage(string route)
        {
            await Shell.Current.GoToAsync(route);
        }
    }
}