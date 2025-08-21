using MauiBankingExercise.Models;
using MauiBankingExercise.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBankingExercise.ViewModels
{

    public partial class CustomerDashboardViewModel : BaseViewModel
    {
        private BankingDatabaseService _bankingDatabaseService;
        private Customer _customer;


        public Customer Customer
        { 
        get { return _customer; }
            set
            {
                if (_customer != value)
                {
                    _customer = value;
                    OnPropertyChanged();
                }
            }
        }

        public CustomerDashboardViewModel(BankingDatabaseService bankingDatabaseService, Customer customer)
        {
            _bankingDatabaseService = bankingDatabaseService;
          
        }

       
    }
}
