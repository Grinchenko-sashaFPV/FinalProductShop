using ClientWPF.Repositories.Implementation.Manager;
using ClientWPF.Repositories.Interfaces;
using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWPF.Repositories.Implementation
{
    
    public class BankAccountsRepository : IBankAccountsRepository
    {
        private readonly ModelsManager _dbManager;
        public BankAccountsRepository()
        {
            _dbManager = new ModelsManager();
        }

        public void AddBankAccount(BankAccount bankAccount)
        {
            _dbManager.BankAccounts.Add(bankAccount);
            _dbManager.SaveChanges();
        }

        public void DeleteBankAccountByUserId(int userId)
        {
            var bankAccount = _dbManager.BankAccounts.Where(b => b.UserId == userId).First();
            _dbManager.BankAccounts.Remove(bankAccount);
            _dbManager.SaveChanges();
        }

        public BankAccount GetBankAccountByUserId(int userId)
        {
            return _dbManager.BankAccounts.FirstOrDefault(b => b.UserId == userId);
        }

        public void UpdateBankAccount(int bankAccountId, BankAccount changedBankAccount)
        {
            var bankAccount = _dbManager.BankAccounts.Where(b => b.Id == bankAccountId).First();
            if(bankAccount != null)
            {
                bankAccount.User = changedBankAccount.User;
                bankAccount.User.Id = changedBankAccount.UserId;
                bankAccount.MoneyAmount = changedBankAccount.MoneyAmount;
                _dbManager.BankAccounts.AddOrUpdate(bankAccount);
                _dbManager.SaveChanges();
            }
        }
    }
    
}
