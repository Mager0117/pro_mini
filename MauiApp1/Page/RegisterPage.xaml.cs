using MauiApp1.ViewModel;

namespace MauiApp1.Page
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage(RegistrationViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}