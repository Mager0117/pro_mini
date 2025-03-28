namespace MauiApp1.Page;

public partial class ViewsPage : ContentPage
{
	public ViewsPage()
	{
		InitializeComponent();
	}
	private async void Button_Clicked(object sender, EventArgs e)
    {
		// DisplayAlert("Alert", "Message", "Close");
		await Navigation.PopAsync();
	}
		private void ImageButton_Clicked(object sender, EventArgs e)
    {
		DisplayAlert("Alert", "Message", "Close");
	}

}