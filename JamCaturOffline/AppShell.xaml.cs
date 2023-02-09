using JamCaturOffline.Views;

namespace JamCaturOffline;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
		Routing.RegisterRoute(nameof(Jam), typeof(Jam));
	}
}

