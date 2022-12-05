using ModelsLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientWPF.Repositories.Interfaces
{
    public interface IBankAccountsRepository
    {
        void DeleteBankAccountByUserId(int userId);
        void UpdateBankAccount(int bankAccountId, BankAccount changedBankAccount);
        void AddBankAccount(BankAccount bankAccount);
        BankAccount GetBankAccountByUserId(int userId);
    }
}
