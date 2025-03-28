



using MauiApp1.ViewModel;

namespace MauiApp1.Page;

public partial class ShowDataPage : ContentPage
{
	public ShowDataPage()
	{
		InitializeComponent();
		BindingContext = new ShowDataViewModel();
	}
}