using MauiBankingExercise.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiBankingExercise.Services
{
    public class BankingDatabaseService
    {
        private readonly SQLiteConnection _db;

        public BankingDatabaseService(SQLiteConnection db)
        {
            _db = db;
        }

        public BankingDatabaseService()
        {
        }

        public List<Customer> GetAllCustomers()
        {
            return _db.Table<Customer>().ToList();
        }

        public Customer GetCustomerById(int customerId)
        {
            return _db.Table<Customer>().FirstOrDefault(c => c.CustomerId == customerId);
        }

        public List<Account> GetAccountsByCustomerId(int customerId)
        {
            return _db.Table<Account>().Where(a => a.CustomerId == customerId).ToList();
        }

        public List<Transaction> GetTransactionsByAccountId(int accountId)
        {
            return _db.Table<Transaction>().Where(t => t.AccountId == accountId)
                .OrderByDescending(t => t.TransactionDate).ToList();

        }

        public void AddTransaction(int transactionId, int transactionTypeId, int accountId, DateTime transactionDate, decimal amount, string description, TransactionType transactionType)
        {
            var account = _db.Table<Account>().FirstOrDefault(a => a.AccountId == accountId);
            if (account == null) return;

            if (transactionTypeId == GetTransactionTypeId("Withdrawal") && account.AccountBalance < amount)
                throw new InvalidOperationException("Insufficient funds for withdrawal.");

            var currentAmount = transactionTypeId == GetTransactionTypeId("Withdrawal") ? -Math.Abs(amount) : amount;

            var transaction = new Transaction
            {
                TransactionId = transactionId,
                TransactionTypeId = transactionTypeId,
                AccountId = accountId,
                TransactionDate = transactionDate,
                Amount = currentAmount,
                Description = description,
                TransactionType = transactionType
            };
            _db.Insert(transaction);

            account.AccountBalance += currentAmount;
            _db.Update(account);
        }
        // Change the access modifier of GetTransactionTypeId from private to public
        public int GetTransactionTypeId(string transactionTypeName)
        {

            var transactionType = _db.Table<TransactionType>().FirstOrDefault(tt => tt.Name == transactionTypeName);

            if (transactionType == null)

                throw new InvalidOperationException($"Transaction type '{transactionTypeName}' not found.");

            return transactionType.TransactionTypeId;

        }

        internal TransactionType GetTransactionTypeId(int transactionTypeId)
        {
            throw new NotImplementedException();
        }

        internal IEnumerable<object> GetCustomerAccounts(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}