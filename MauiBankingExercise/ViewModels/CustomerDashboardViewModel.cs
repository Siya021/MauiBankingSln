using GalaSoft.MvvmLight.Command;
using MauiBankingExercise.Models;
using MauiBankingExercise.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.WebUI;

namespace MauiBankingExercise.ViewModels
{

    public partial class CustomerDashboardViewModel : BaseViewModel
    {

        private readonly BankingDatabaseService _db;

        // Replacing [ObservableProperty] with standard property implementation
        private ObservableCollection<Account> accounts;
        public ObservableCollection<Account> Accounts
        {
            get => accounts;
            set => SetProperty(ref accounts, value);
        }

        private Account selectedAccount;
        public Account SelectedAccount
        {
            get => selectedAccount;
            set => SetProperty(ref selectedAccount, value);
        }

        private decimal transactionAmount;
        public decimal TransactionAmount
        {
            get => transactionAmount;
            set => SetProperty(ref transactionAmount, value);
        }

        private string selectedTransactionType;
        public string SelectedTransactionType
        {
            get => selectedTransactionType;
            set => SetProperty(ref selectedTransactionType, value);
        }

        public ObservableCollection<string> TransactionTypes { get; } = new ObservableCollection<string>
            {
                "Deposit",
                "Withdrawal"
            };

        public CustomerDashboardViewModel(BankingDatabaseService db, int customerId, ObservableCollection<Account> accounts)
        {
            _db = db;
            Accounts = new ObservableCollection<Account>(_db.GetAccountsByCustomerId(customerId));
        }

        [RelayCommand]
        void MakeTransaction()
        {
            if (selectedAccount == null || string.IsNullOrEmpty(selectedTransactionType)) return;

            var transactionTypeId = _db.GetTransactionTypeId(selectedTransactionType);

            try
            {
                _db.AddTransaction(0, transactionTypeId, selectedAccount.AccountId, DateTime.Now, TransactionAmount, "Transaction made from dashboard", null);
            }
            catch (InvalidOperationException ex)
            {
                // Handle insufficient funds or other exceptions
                Console.WriteLine(ex.Message);// Assuming 1 for Deposit and 2 for Withdrawal
            }
        }
    }
