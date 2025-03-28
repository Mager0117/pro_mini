using MauiApp1.Page;

namespace MauiApp1;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		
		Routing.RegisterRoute("register", typeof(RegisterPage));
		//Routing.RegisterRoute("ShowDataPage", typeof(ShowDataPage));
		// Routing.RegisterRoute("addcourses", typeof(AddCoursesPage));

		Routing.RegisterRoute(nameof(DeleteCoursesPage), typeof(DeleteCoursesPage));
		Routing.RegisterRoute(nameof(AddCoursesPage), typeof(AddCoursesPage));
		Routing.RegisterRoute(nameof(ShowDataPage), typeof(ShowDataPage));
		//Routing.RegisterRoute("ShowObjectsPage", typeof(ShowObjectsPage));
		Routing.RegisterRoute(nameof(ShowObjectsPage), typeof(ShowObjectsPage));

		Routing.RegisterRoute(nameof(HistoryPage), typeof(HistoryPage));

		
	}
}
