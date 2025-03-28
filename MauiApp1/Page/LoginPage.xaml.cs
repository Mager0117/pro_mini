using System;
using MauiApp1.ViewsModel;


namespace MauiApp1.Page;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewsModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

	private async void Button_Clicked(object sender, EventArgs e)
	{
		// System.Diagnostics.Debug.WriteLine(uname.Text);
		// System.Diagnostics.Debug.WriteLine(pwd.Text);
		// if (uname.Text == "admin" && pwd.Text == "12")
		// {
		// 	await Navigation.PushAsync(new ViewsPage());
		// }
		// else
		// {
		// 	await DisplayAlert("Error", "Username or password incorrect", "ปิด");
		// }
	}

	private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
	{
		// System.Diagnostics.Debug.WriteLine("Hello");
		// System.Diagnostics.Debug.WriteLine(uname.Text);

		// var loginData = new LoginData {
		// 	Uname = uname.Text, Pwd = pwd.Text
		// };
		// var queryParams = new Dictionary< string, object>(){
		// 	{"data", loginData}
		// };
		// //await Shell.Current.GoToAsync($"forgetpassword?uname={uname.Text}&pwd={pwd.Text}");
		// //await Shell.Current.GoToAsync($"{nameof(ForgetPasswordPage)}?uname={uname.Text}&pwd={pwd.Text}");
		// await Shell.Current.GoToAsync($"{nameof(ForgetPasswordPage)}", queryParams);
	}
	
}
public class LoginData
{
	public String Uname {get; set;} = "";
	public String Pwd {get; set;} = "";
}