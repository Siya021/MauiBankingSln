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
    public partial class CustomerSelectionViewModel : BaseViewModel
    {
        private BankingDatabaseService _bankingDatabaseService;

        public ICommand MyButtonCommand { get; set; }

        private BankingDatabaseService _customerDatabaseService;
        private ObservableCollection<Customer> _customers;

        public ObservableCollection<Customer> Customers
        {
            get { return _customers; }
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
            MyButtonCommand = new Command(MyButtonAction);
            CustomerSelectedCommand = new Command<Customer>(async (customer) => await CustomerSelected(customer));
        }

        private void MyButtonAction(object obj)
        {
            // Implement your button logic here
        }

        // Removed [RelayCommand] attribute
        public async Task CustomerSelected(Customer customer)
        {
            var navigationParameter = new Dictionary<string, object>
                {
                    { "Customer", customer }
                };
            await Shell.Current.GoToAsync($"customerDetails", navigationParameter);
        }

        // Replace all references to _customerDatabaseService with _bankingDatabaseService
        public override void OnAppearing()
        {
            base.OnAppearing();
           
            Customers = new ObservableCollection<Customer>(_bankingDatabaseService.GetAllCustomers());
        }
       
        public List<Customer> GetAllCustomers()
        {
         
            return new List<Customer>();
        }
    }
}
