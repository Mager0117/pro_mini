using System.Collections.ObjectModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp1.Page;
using MauiApp1.Model;
using MauiApp1.Services;
using MauiApp2.Model;
using System.Linq;
using System.Diagnostics;

namespace MauiApp1.ViewModel;

public partial class AddCourseViewModel : ObservableObject
{
    private readonly CourseService _courseService;
    
    [ObservableProperty]
    private ObservableCollection<Courses> _courseList = new();
    
    [ObservableProperty]
    private ObservableCollection<Courses> _filteredCourseList = new();
    
    [ObservableProperty]
    private string _searchText;
    
    [ObservableProperty]
    private bool _isLoading;
    
    [ObservableProperty]
    private string _statusMessage;
    
    public ICommand AddCourseCommand { get; private set; }
    public ICommand ShowAllCoursesCommand { get; private set; }
    public ICommand EnrollCourseCommand { get; private set; }
    public int UserId { get; set; }

    public AddCourseViewModel(CourseService courseService)
    {
        _courseService = courseService;
        AddCourseCommand = new Command(async () => await ExecuteAddCourseCommand());
        ShowAllCoursesCommand = new RelayCommand(ExecuteShowAllCoursesCommand);
        EnrollCourseCommand = new RelayCommand<Courses>(async (course) => await ExecuteEnrollCourseCommand(course));
        
        _ = LoadCoursesAsync();
    }

    partial void OnSearchTextChanged(string value)
    {
        FilterCourses(value);
    }

    private void FilterCourses(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            FilteredCourseList = new ObservableCollection<Courses>(CourseList);
            return;
        }

        var filtered = CourseList.Where(c => 
            c.CourseName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
            c.CourseId.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .ToList();

        FilteredCourseList = new ObservableCollection<Courses>(filtered);
    }

    private void ExecuteShowAllCoursesCommand()
    {
        FilteredCourseList = new ObservableCollection<Courses>(CourseList);
        SearchText = string.Empty;
    }

    private async Task ExecuteAddCourseCommand()
    {
        if (UserId > 0)
        {
            await Shell.Current.GoToAsync($"{nameof(AddCoursesPage)}?userId={UserId}");
        }
        else
        {
            Console.WriteLine("Invalid UserId");
        }
    }

 private async Task ExecuteEnrollCourseCommand(Courses course)
{
    if (UserId <= 0)
    {
        StatusMessage = "กรุณาเข้าสู่ระบบก่อนลงทะเบียน";
        return;
    }

    IsLoading = true;
    StatusMessage = "กำลังดำเนินการ...";
    
    try
    {
        var success = await _courseService.EnrollCourseAsync(UserId.ToString(), course.CourseId);
        
        if (success)
        {
            StatusMessage = $"ลงทะเบียนวิชา {course.CourseName} สำเร็จ";
            
            // กลับไปหน้าก่อนหน้าและส่งพารามิเตอร์ให้รีเฟรช
            await Shell.Current.GoToAsync($"..?refresh=true&userId={UserId}");
        }
        else
        {
            StatusMessage = $"คุณได้ลงทะเบียนวิชา {course.CourseName} ไว้แล้ว";
        }
    }
    catch (Exception ex)
    {
        StatusMessage = $"เกิดข้อผิดพลาด: {ex.Message}";
        Debug.WriteLine($"Error enrolling course: {ex}");
    }
    finally
    {
        IsLoading = false;
        await Task.Delay(3000);
        StatusMessage = string.Empty;
    }
}

    private async Task LoadCoursesAsync()
    {
        IsLoading = true;
        try
        {
            var courses = await _courseService.LoadCoursesAsync();
            if (courses != null)
            {
                CourseList.Clear();
                foreach (var course in courses)
                {
                    CourseList.Add(course);
                }
                
                FilteredCourseList = new ObservableCollection<Courses>(CourseList);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Error loading courses: {ex}");
            StatusMessage = "ไม่สามารถโหลดข้อมูลวิชาได้";
            await Task.Delay(3000);
            StatusMessage = string.Empty;
        }
        finally
        {
            IsLoading = false;
        }
    }
}