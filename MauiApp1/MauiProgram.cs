using MauiApp1.Page;
using MauiApp1.Services;
using MauiApp1.ViewModel;
using MauiApp1.ViewsModel;


namespace MauiApp1;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // ลงทะเบียน StudentService เป็น Singleton
        builder.Services.AddSingleton<StudentService>();
        builder.Services.AddSingleton<CourseService>();
       
        // ลงทะเบียน ViewModels
        builder.Services.AddTransient<LoginViewsModel>();
        builder.Services.AddTransient<ShowObjectsViewModel>();
        builder.Services.AddTransient<RegistrationViewModel>();
        builder.Services.AddTransient<AddCourseViewModel>();
        builder.Services.AddTransient<DeleteCoursesViewModel>();
        // ลงทะเบียน Pages
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<ShowObjectsPage>();
        builder.Services.AddTransient<RegisterPage>();
        builder.Services.AddTransient<AddCoursesPage>();
        builder.Services.AddTransient<DeleteCoursesPage>();



     
        builder.Services.AddSingleton<HistoryService>();
        builder.Services.AddTransient<HistoryViewModel>();
        builder.Services.AddTransient<HistoryPage>();

        

        return builder.Build();
    }
}