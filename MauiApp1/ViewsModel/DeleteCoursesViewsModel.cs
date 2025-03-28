using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Model;
using MauiApp1.Services;
using MauiApp2.Model;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MauiApp1.ViewModel
{
    public partial class DeleteCoursesViewModel : ObservableObject
    {
        private readonly StudentService _studentService;
        private readonly CourseService _courseService;

        [ObservableProperty]
        private Profile _studentProfile;

        [ObservableProperty]
        private Term _currentTerm;

        [ObservableProperty]
        private ObservableCollection<EnrolledCourse> _enrolledCourses = new();

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private string _statusMessage;

        [ObservableProperty]
        private string _userId; // เพิ่ม property เก็บ studentId

        public DeleteCoursesViewModel(StudentService studentService, CourseService courseService)
        {
            _studentService = studentService;
            _courseService = courseService;
        }

        [RelayCommand]
        public async Task LoadDataAsync(string studentId)
        {
            UserId = studentId; // เก็บ studentId ไว้ใช้ใน ViewModel
            IsLoading = true;
            try
            {
                var student = await _studentService.GetStudentByIdAsync(studentId);
                if (student != null)
                {
                    StudentProfile = student.Profile;
                    CurrentTerm = student.CurrentTerm;
                    await LoadEnrolledCourses(student.CurrentTerm.EnrolledCourses);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading data: {ex.Message}");
                StatusMessage = "เกิดข้อผิดพลาดในการโหลดข้อมูล";
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task LoadEnrolledCourses(List<string> courseIds)
        {
            try
            {
                var courses = await _courseService.LoadCoursesAsync();
                var enrolled = new ObservableCollection<EnrolledCourse>();

                foreach (var courseId in courseIds)
                {
                    var course = courses.FirstOrDefault(c => c.CourseId == courseId);
                    if (course != null)
                    {
                        enrolled.Add(new EnrolledCourse
                        {
                            CourseId = course.CourseId,
                            CourseName = course.CourseName,
                            Credits = (int)course.Credits
                        });
                    }
                }

                EnrolledCourses = enrolled;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error loading enrolled courses: {ex.Message}");
                throw;
            }
        }

       [RelayCommand]
public async Task DropCourse(string courseId)
{
    if (string.IsNullOrEmpty(UserId))
    {
        StatusMessage = "ไม่พบรหัสนักศึกษา";
        return;
    }

    IsLoading = true;
    StatusMessage = "กำลังดำเนินการถอนวิชา...";
    
    try
    {
        var success = await _courseService.DropCourseAsync(UserId, courseId);

        if (success)
        {
            StatusMessage = "ถอนวิชาสำเร็จ";
            
            // โหลดข้อมูลใหม่หลังถอนวิชา
            await LoadDataAsync(UserId);
            
            // แจ้งเตือนให้หน้าอื่นรู้ว่ามีการเปลี่ยนแปลง
            MessagingCenter.Send(this, "CoursesUpdated");
            
            // กลับไปหน้าก่อนหน้า (ShowObjectsPage)
            await Shell.Current.GoToAsync("..");
        }
        else
        {
            StatusMessage = "ถอนวิชาไม่สำเร็จ";
        }
    }
    catch (Exception ex)
    {
        Debug.WriteLine($"Error dropping course: {ex.Message}");
        StatusMessage = "เกิดข้อผิดพลาดในการถอนวิชา";
    }
    finally
    {
        IsLoading = false;
        await Task.Delay(3000);
        StatusMessage = string.Empty;
    }
}}
}