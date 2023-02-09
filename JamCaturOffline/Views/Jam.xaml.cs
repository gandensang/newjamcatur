using JamCaturOffline.ViewModels;

namespace JamCaturOffline.Views;

public partial class Jam : ContentPage
{
	public Jam(JamVM vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}
