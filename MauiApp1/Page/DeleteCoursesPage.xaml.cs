using System.Diagnostics;
using MauiApp1.Services;
using MauiApp1.ViewModel;


namespace MauiApp1.Page
{
    [QueryProperty(nameof(UserId), "userId")]
    public partial class DeleteCoursesPage : ContentPage
    {
        private DeleteCoursesViewModel _viewModel;
        private string _userId;

        public string UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                Debug.WriteLine($"UserId set: {value}");
                LoadData();
            }
        }


        public DeleteCoursesPage()
        {
            InitializeComponent();
            _viewModel = new DeleteCoursesViewModel(
                new StudentService(),
                new CourseService());
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // สมัครรับการแจ้งเตือนเมื่อมีการอัปเดตวิชา
            MessagingCenter.Subscribe<DeleteCoursesViewModel>(this, "CoursesUpdated", async (sender) =>
            {
                if (!string.IsNullOrEmpty(UserId))
                {
                    await _viewModel.LoadDataAsync(UserId);
                }
            });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<DeleteCoursesViewModel>(this, "CoursesUpdated");
        }

        private async void LoadData()
        {
            Debug.WriteLine($"Loading data for user: {UserId}");
            if (!string.IsNullOrEmpty(UserId))
            {
                await _viewModel.LoadDataAsync(UserId);

                // Debug output
                Debug.WriteLine($"Student Name: {_viewModel.StudentProfile?.Name}");
                Debug.WriteLine($"Term: {_viewModel.CurrentTerm?.TermTerm}");
                Debug.WriteLine($"Enrolled Courses Count: {_viewModel.EnrolledCourses?.Count}");
            }
        }
    }
}