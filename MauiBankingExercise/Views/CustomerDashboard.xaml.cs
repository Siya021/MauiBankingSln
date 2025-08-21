using MauiBankingExercise.ViewModels;

namespace MauiBankingExercise.Views;

public partial class CustomerDashboard : ContentPage
{
	public CustomerDashboard(CustomerDashboardViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}

    protected override void OnAppearing()
    {
        
		base.OnAppearing();

		((CustomerDashboardViewModel)BindingContext).OnAppearing();
    }
}