using ATMapp.Domain.Entities;
using ATMapp.Domain.Enums;
using ATMapp.Domain.Interfaces;
using ATMapp.UI;


namespace ATMapp.App;

public class ATMApp : IUserLogin, IUserAccountActions, ITransaction
{
    private List<UserAccount> userAccountList;

    private UserAccount selectedAccount;

    private List<Transaction> listOfTransaction;

    private const decimal minimumKeptAmount = 500;

    public void Run()
    {
        AppScreen.Welcome();
        CheckUserCardNumberAndPassword();
        AppScreen.WelcomeCustomer(selectedAccount.FullName);
        AppScreen.DisplayAppMenu();
        ProcessMenuOptions();
        
    }

    public void InitializeData()
    {
        userAccountList = new List<UserAccount>
        {
            new UserAccount(1, 123456, 123123, 123123123, "Per", 10000.00m, 0,
                false),
            new UserAccount(2, 223344, 123123, 123123124, "Pål", 15000.50m, 0,
                false),
            new UserAccount(3, 334455, 123000, 123123125, "Peter", 20000.90m, 0,
                false),
        };
        listOfTransaction = new List<Transaction>();
    }

    public void CheckUserCardNumberAndPassword()
    {
        bool IsCorrectLogin = false;

        while (IsCorrectLogin == false)
        {
            UserAccount inputAccount = AppScreen.UserLoginForm();
            AppScreen.LogInProgress();

            foreach (UserAccount account in userAccountList)
            {
                selectedAccount = account;
                if (inputAccount.CardNumber.Equals(selectedAccount.CardNumber))
                {
                    selectedAccount.TotalLogin++;
                    if (inputAccount.CardPin.Equals(selectedAccount.CardPin))
                    {
                        selectedAccount = account;

                        if (selectedAccount.IsLocked || selectedAccount.TotalLogin > 3)
                        {
                            AppScreen.PrintLockScreen();
                        }
                        else
                        {
                            selectedAccount.TotalLogin = 0;
                            IsCorrectLogin = true;
                            break;
                        }
                    }
                }
                
                if (IsCorrectLogin == false)
                {
                    Utility.PrintMessage("\ninvalid card number or PIN.", false);
                    selectedAccount.IsLocked = selectedAccount.TotalLogin == 3;
                    if (selectedAccount.IsLocked)
                    {
                        AppScreen.PrintLockScreen();
                    }
                }
                Console.Clear();
            }
        }

        
    }

    private void ProcessMenuOptions()
    {
        switch (Validator.Convert<int>("option"))
        {
            case (int)AppMenu.CheckBalance:
                CheckBalance();
                break;
            case (int)AppMenu.PlaceDeposits:
                PlaceDeposit();
                break;
            case (int)AppMenu.MakeWithdrawal:
                MakeWithdrawal();
                break;
            case (int)AppMenu.InternalTransfer:
                Console.WriteLine("making transfer...");
                break;
            case (int)AppMenu.ViewTransaction:
                Console.WriteLine("view transactions...");
                break;
            case (int)AppMenu.Logout:
                AppScreen.LouOutProgress();
                Utility.PrintMessage("You have successfully loggeg out. Please collect your ATM card.");
                Run();
                break;
            default:
                Utility.PrintMessage("invalid option!!!",false);
                break;
        }
    }


    public void CheckBalance()
    {
        Utility.PrintMessage($"Your account balance is: {Utility.FormatAmount(selectedAccount.AccountBalance)}");
    }

    public void PlaceDeposit()
    {
        Console.WriteLine("Only multiple of 500 and 1000 Kr are allowed.");
        var transaction_amt = Validator.Convert<int>($"amount {AppScreen.cur}");
        
        Console.WriteLine("Checking and counting bank notes.");
        Utility.PrintDotAnimation();
        Console.WriteLine("");
        
        if(transaction_amt<=0)
        {
            Utility.PrintMessage("Amount needs to be greater than zero. Try again.");
            return;
        }

        if (transaction_amt%500 !=0)
        {
            Utility.PrintMessage("Enter deposit amount in multiples of 500 or 1000. Try again.");
            return;
        }

        if (PreviewBankNotesCount(transaction_amt)==false)
        {
            Utility.PrintMessage("You have cancelled your action",false);
        }
        
        //bind transaction details to transaction object
        InsertTransaction(selectedAccount.Id,TransactionType.Deposits,
            transaction_amt,"");
        
        //update account balance
        selectedAccount.AccountBalance += transaction_amt;
        
        //print success message
        Utility.PrintMessage($"Your deposits of {Utility.FormatAmount
            (transaction_amt)} was successful.",true);



    }

    public void MakeWithdrawal()
    {
        var transactionAmt = 0;
        int selectedAmount = AppScreen.SelectAmount();
        if (selectedAmount == -1)
        {
            selectedAmount = AppScreen.SelectAmount();
        }else if (selectedAmount != 0)
        {
            transactionAmt = selectedAmount;
        }
        else
        {
            transactionAmt = Validator.Convert<int>($"amount {AppScreen.cur}");
        }
        
        //input validation
        if (transactionAmt <= 0)
        {
            Utility.PrintMessage($"Amount needs to be greater than zero. Try again.", false);
            return;
        }

        if (transactionAmt % 500 !=0)
        {
            Utility.PrintMessage($"You can only withdraw amount in multiple of 500 or 1000 Kr.", false);
            return;
        }
        
        //Business logic validations
        if (transactionAmt > selectedAccount.AccountBalance)
        {
            Utility.PrintMessage($"Withdrawal failed. Your balance is too low to withdraw {Utility.FormatAmount(transactionAmt)}",false);
            return;
        }

        if (selectedAccount.AccountBalance - transactionAmt < minimumKeptAmount)
        {
            Utility.PrintMessage($"Withdrawal failed. Your account needs to have minimum {Utility.FormatAmount(minimumKeptAmount)}",false);
            return;
        }
        
        //Bind withdrawal details to transaction object
        InsertTransaction(selectedAccount.Id,TransactionType.Withdrawal,-transactionAmt,"");
        
        //update account balance
        selectedAccount.AccountBalance -= transactionAmt;
        
        //success message
        Utility.PrintMessage($"You have successfully withdrawn {Utility.FormatAmount(transactionAmt)}",true);


    }

    private bool PreviewBankNotesCount(int amount)
    {
        int thousandNotesCount = amount / 1000;
        int fiveHundredNotesCount = (amount % 1000) / 500;
        Console.WriteLine("Summary:");
        Console.WriteLine($"{AppScreen.cur}1000 X {thousandNotesCount} = {1000 * thousandNotesCount}");
        Console.WriteLine($"{AppScreen.cur}500 X {fiveHundredNotesCount} = {500 * fiveHundredNotesCount}");
        Console.WriteLine($"Total amount: {Utility.FormatAmount(amount)}\n");

        int opt = Validator.Convert<int>("1 to confirm");
        return opt.Equals(1);
    }


    public void InsertTransaction(long userBankAccountId, TransactionType transType,
        decimal transAmount, string desc)
    {
        var transaction = new Transaction()
        {
            TransactionId = Utility.GetTransactionId(),
            UserBankAccountID = userBankAccountId,
            TransactionDate = DateTime.Now,
            TransactionType = transType,
            TransactionAmount = transAmount,
            Description = desc,
        };
        
        listOfTransaction.Add(transaction);
        
    }

    public void ViewTransaction()
    {
        throw new NotImplementedException();
    }
    
}