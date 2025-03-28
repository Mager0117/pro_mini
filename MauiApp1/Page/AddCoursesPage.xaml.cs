using MauiApp1.ViewModel;
using MauiApp1.Services;

namespace MauiApp1.Page
{
    [QueryProperty(nameof(UserId), "userId")]
    public partial class AddCoursesPage : ContentPage
    {
        private readonly AddCourseViewModel _viewModel;

        public int UserId
        {
            set
            {
                _viewModel.UserId = value; // อัปเดตค่า UserId ใน ViewModel
            }
        }


        public AddCoursesPage()
        {
            InitializeComponent();
            _viewModel = new AddCourseViewModel(new CourseService());
            BindingContext = _viewModel;
        }
    }
}
