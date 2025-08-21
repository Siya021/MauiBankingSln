using MauiBankingExercise.Services;
using MauiBankingExercise.ViewModels;

namespace MauiBankingExercise.Views;

public partial class CustomerSelectionView : ContentPage
{
	public CustomerSelectionView(CustomerSelectionViewModel vm)
	{
		InitializeComponent();


		BindingContext = vm;
	}

	protected override void OnAppearing()
	{ 
		base.OnAppearing();

		((CustomerSelectionViewModel)BindingContext).OnAppearing();

	
	}
}