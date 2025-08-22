using GalaSoft.MvvmLight.Command;
using MauiBankingExercise.Models;
using MauiBankingExercise.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiBankingExercise.ViewModels
{
    public partial class CustomerDashboardViewModel : BaseViewModel
    {
        private readonly BankingDatabaseService _db;
        private Customer _customer = null!;
        private Account _selectedAccount = null!;
        private string _selectedTransactionType = string.Empty;
        private decimal _transactionAmount;
        private bool _isTransactionButtonEnabled;
        private int _customerId;

        public ObservableCollection<Account> Accounts { get; set; } = new ObservableCollection<Account>();
        public ObservableCollection<Transaction> CurrentTransaction { get; set; } = new ObservableCollection<Transaction>();
        public ObservableCollection<string> TransactionTypes { get; } = new ObservableCollection<string>
        {
            "Deposit",
            "Withdrawal"
        };

        public ICommand SubmitTransactionCommand { get; }
        public ICommand ViewTransactionsCommand { get; }

        public int CustomerId
        {
            get => _customerId;
            set
            {
                _customerId = value;
                OnPropertyChanged(nameof(CustomerId));
            }
        }

        public Customer Customer
        {
            get => _customer;
            set
            {
                _customer = value;
                OnPropertyChanged(nameof(Customer));
            }
        }

        public string CustomerFullName => Customer == null ? "Customer Not Found" : $"{Customer.FirstName} {Customer.LastName}";

        public Account SelectedAccount
        {
            get => _selectedAccount;
            set
            {
                _selectedAccount = value;
                OnPropertyChanged(nameof(SelectedAccount));
                LoadTransactions();
            }
        }

        public string SelectedTransactionType
        {
            get => _selectedTransactionType;
            set
            {
                _selectedTransactionType = value;
                OnPropertyChanged(nameof(SelectedTransactionType));
                IsTransactionButtonEnabled = !string.IsNullOrEmpty(_selectedTransactionType) && _transactionAmount > 0;
            }
        }

        public decimal TransactionAmount
        {
            get => _transactionAmount;
            set
            {
                _transactionAmount = value;
                OnPropertyChanged(nameof(TransactionAmount));
                IsTransactionButtonEnabled = !string.IsNullOrEmpty(_selectedTransactionType) && _transactionAmount > 0;
            }
        }

        public bool IsTransactionButtonEnabled
        {
            get => _isTransactionButtonEnabled;
            set
            {
                _isTransactionButtonEnabled = value;
                OnPropertyChanged(nameof(IsTransactionButtonEnabled));
            }
        }

        public CustomerDashboardViewModel(BankingDatabaseService db)
        {
            _db = db;
            Accounts = new ObservableCollection<Account>();
            SubmitTransactionCommand = new RelayCommand(SubmitTransaction, CanSubmitTransaction);
            ViewTransactionsCommand = new RelayCommand(LoadTransactions);
        }

        public CustomerDashboardViewModel()
        {
            _db = null!;
            Accounts = new ObservableCollection<Account>();
            SubmitTransactionCommand = null!;
            ViewTransactionsCommand = null!;
            }

        private void LoadTransactions()
        {
            if (SelectedAccount != null)
            {
                CurrentTransaction.Clear();
                var transactions = _db.GetTransactionsByAccountId(SelectedAccount.AccountId);
                foreach (var tx in transactions)
                {
                    CurrentTransaction.Add(tx);
                }
            }
        }

        private void SubmitTransaction()
        {
        }

        private bool CanSubmitTransaction()
        {
            return SelectedAccount != null && !string.IsNullOrEmpty(SelectedTransactionType) && TransactionAmount > 0;
        }
    }
}
