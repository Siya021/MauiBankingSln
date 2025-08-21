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
        private SQLiteConnection _db;
      
        public BankingDatabaseService (SQLiteConnection db)
        {
            _db = db;
        }

        public List<Customer> GetAllCustomers()
        {
            return _db.Table<Customer>().ToList();
        }

        public Customer GetCustomerById(int customerId)
        {
            return _db.Table<Customer>().FirstOrDefault(c => c.CustomerId == customerId);
        }


    }
}
