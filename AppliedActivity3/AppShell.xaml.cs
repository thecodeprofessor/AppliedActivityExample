namespace AppliedActivity3;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(Views.MainPage), typeof(Views.MainPage));
        Routing.RegisterRoute(nameof(Views.StudentPage), typeof(Views.StudentPage));
        Routing.RegisterRoute(nameof(Views.StudentsPage), typeof(Views.StudentsPage));
        Routing.RegisterRoute(nameof(Views.CoursePage), typeof(Views.CoursePage));
        Routing.RegisterRoute(nameof(Views.CoursesPage), typeof(Views.CoursesPage));
    }
}
