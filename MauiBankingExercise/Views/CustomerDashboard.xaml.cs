using MauiBankingExercise.ViewModels;

namespace MauiBankingExercise.Views;

public partial class CustomerDashboard : ContentPage
{
    private readonly CustomerDashboardViewModel vm;

    public CustomerDashboard()
    {
        InitializeComponent();
        vm = new CustomerDashboardViewModel();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }
}
