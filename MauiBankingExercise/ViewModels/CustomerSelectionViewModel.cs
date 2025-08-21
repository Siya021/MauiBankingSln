using MauiBankingExercise.Models;
using MauiBankingExercise.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiBankingExercise.ViewModels
{
    public partial class CustomerSelectionViewModel : BaseViewModel // Assuming you have a BaseViewModel
    {
        private readonly BankingDatabaseService _bankingDatabaseService;
        private ObservableCollection<Customer> _customers;

        public ObservableCollection<Customer> Customers
        {
            get => _customers;
            set
            {
                _customers = value;
                OnPropertyChanged();
            }
        }

        public ICommand CustomerSelectedCommand { get; }

        public CustomerSelectionViewModel(BankingDatabaseService bankingDatabaseService)
        {
            _bankingDatabaseService = bankingDatabaseService;
            
            CustomerSelectedCommand = new Command(CustomerSelected);
        }

        private void CustomerSelected(object obj)
        {

        }

        public async Task CustomerSelected(Customer customer)
        {
            if (customer == null)
                return;

            var navigationParameter = new Dictionary<string, object>
            {
                { "Customer", customer }
            };
            if (Shell.Current != null)
                await Shell.Current.GoToAsync("customerDetails", navigationParameter);
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            var customers = _bankingDatabaseService.GetAllCustomers();
            Customers = new ObservableCollection<Customer>(customers ?? new List<Customer>());
        }
    }
}
