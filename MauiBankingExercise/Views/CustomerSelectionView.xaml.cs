using MauiBankingExercise.ViewModels;

namespace MauiBankingExercise.Views;

public partial class CustomerSelectionView : ContentPage
{
    public CustomerSelectionView()
    {
        InitializeComponent();
        BindingContext = new CustomerDashboardViewModel();
    }
}