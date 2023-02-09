using JamCaturOffline.ViewModels;

namespace JamCaturOffline.Views;

public partial class HomePage : ContentPage
{
	public HomePage(HomePageVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

}
