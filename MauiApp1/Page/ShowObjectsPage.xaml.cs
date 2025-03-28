using System.Diagnostics;
using MauiApp1.Services;
using MauiApp1.ViewModel;

namespace MauiApp1.Page
{
    [QueryProperty(nameof(UserId), "userId")]
    public partial class ShowObjectsPage : ContentPage
    {
        private ShowObjectsViewModel ViewModel;

        public ShowObjectsPage(ShowObjectsViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
            BindingContext = ViewModel;
        }

        private string _userId;
        public string UserId
        {
            get => _userId;
            set
            {
                _userId = value;
                OnUserIdReceived(value);
            }
        }



        private async void OnUserIdReceived(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                Debug.WriteLine($"Received User ID: {userId}");
                ViewModel.UserId = userId;
                await ViewModel.LoadDataAsync();
            }
            else
            {
                Debug.WriteLine("UserId is null or empty.");
            }
        }
        private async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(UserId))
            {
                await Shell.Current.GoToAsync($"{nameof(AddCoursesPage)}?userId={UserId}");
                // โหลดข้อมูลใหม่เมื่อกลับมาจากหน้าเพิ่มวิชา
                await ViewModel.LoadDataAsync(true);
            }
            else
            {
                Debug.WriteLine("UserId is null or empty. Cannot navigate.");
            }
        }

        private async void deletecoursesButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(UserId))
            {
                await Shell.Current.GoToAsync($"{nameof(DeleteCoursesPage)}?userId={UserId}");
                // โหลดข้อมูลใหม่เมื่อกลับมาจากหน้าลบวิชา
                await ViewModel.LoadDataAsync(true);
            }
            else
            {
                Debug.WriteLine("UserId is null or empty. Cannot navigate.");
            }
        }
        private async void OnViewHistoryClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(UserId))
            {
                await Navigation.PushAsync(new HistoryPage(new HistoryViewModel(new HistoryService()))
                {
                    UserId = UserId
                });
            }
        }
    }




}