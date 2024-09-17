using System.Net.Sockets;
using ATMapp.Domain.Enums;

namespace ATMapp.Domain.Entities;

public class Transaction
{
    public long TransactionId { get; set; }
    public long UserBankAccountID { get; set; }
    public DateTime TransactionDate { get; set; }
    public TransactionType TransactionType { get; set; }
    public string Description { get; set; }
    public Decimal TransactionAmount { get; set; }



}