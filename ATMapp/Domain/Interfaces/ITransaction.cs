using ATMapp.Domain.Entities;
using ATMapp.Domain.Enums;

namespace ATMapp.Domain.Interfaces;

public interface ITransaction
{
    void InsertTransaction(long userBankAccountId, TransactionType transType, decimal transAmount, string desc);
    void ViewTransaction();
    
}