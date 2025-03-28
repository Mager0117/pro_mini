using MauiApp1.ViewModel;

namespace MauiApp1.Page
{
    [QueryProperty(nameof(UserId), "userId")]
    public partial class HistoryPage : ContentPage
    {
        private HistoryViewModel ViewModel => (HistoryViewModel)BindingContext;
        
        public string UserId { get; set; }

        public HistoryPage(HistoryViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (!string.IsNullOrEmpty(UserId))
            {
                ViewModel.UserId = UserId;
                await ViewModel.LoadHistoryAsync();
            }
        }
    }
}