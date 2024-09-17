using ATMapp.Domain.Entities;

namespace ATMapp.UI;

public  class AppScreen
{
    internal const string cur = "Kr ";
    internal static void Welcome()
    {
        //clear the console
        Console.Clear();

        //sets the title of the console window
        Console.Title = "ATM App";

        //sets the text color to DarkBlue
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        
        Console.WriteLine("\n\t\t\t\t\t ********** Welcome to ATM App **********");
        
        //prompt the user to insert atm card
        Console.WriteLine("\nPlease insert your ATM card");

        Utility.PressEnterToContinue();
    }
    
    
    internal static UserAccount UserLoginForm()
    {
        UserAccount tempUserAccount = new UserAccount();

        tempUserAccount.CardNumber = Validator.Convert<long>("card number");
        tempUserAccount.CardPin = Convert.ToInt32(Utility.GetSecretInput("enter your card PIN: "));
        return tempUserAccount;
    }

    internal static void LogInProgress()
    {
        Console.WriteLine("\nChecking card number and PIN...");
        Utility.PrintDotAnimation();
    }

    internal static void PrintLockScreen()
    {
        Console.Clear();
        Utility.PrintMessage("Your account is locked. Please call customer service or go to the nearest branch to unlock your account. Thank you.", true);
        Utility.PressEnterToContinue();
        Environment.Exit(1);
    }

    internal static void WelcomeCustomer(string fullName)
    {
        Console.WriteLine($"welcome back, {fullName}");
        Utility.PressEnterToContinue();
    }

    internal static void DisplayAppMenu()
    {
        Console.Clear();
        Console.WriteLine("********** My ATM App Menu **********");
        Console.WriteLine("1. Account Balance                  :");
        Console.WriteLine("2. Cash Deposit                     :");
        Console.WriteLine("3. Withdrawal                       :");
        Console.WriteLine("4. Transfer                         :");
        Console.WriteLine("5. Transactions                     :");
        Console.WriteLine("6. Logout                           :");
        
    }

    internal static void LouOutProgress()
    {
        Console.WriteLine("Thank you for using My ATM app.");
        Utility.PrintDotAnimation();
        Console.Clear();
    }

    internal static int SelectAmount()
    {
        Console.WriteLine("");
        Console.WriteLine("1.{0}500",cur);
        Console.WriteLine("2.{0}1000",cur);
        Console.WriteLine("3.{0}2000",cur);
        Console.WriteLine("4.{0}5000",cur);
        Console.WriteLine("5.{0}10000",cur);
        Console.WriteLine("6.{0}20000",cur);
        Console.WriteLine("0.other");
        Console.WriteLine("");
        int selectedAmount = Validator.Convert<int>("option:");

        switch (selectedAmount)
        {
            case 1:
                return 500;
            break;
            case 2:
                return 1000;
                break;
            case 3:
                return 2000;
                break;
            case 4:
                return 5000;
                break;
            case 5:
                return 10000;
                break;
            case 6:
                return 20000;
                break;
            case 0:
                return 0;
                break;
            default:
                Utility.PrintMessage("Invalid input. Try again.",false);
                SelectAmount();
                return -1;
            break;
            
        }
        
    }

    internal InternalTransfer InternalTransferForm()
    {
        var internalTransfer = new InternalTransfer();
        internalTransfer.RecipientBankAccountNumber = Validator.Convert<long>("recipient account number");
        internalTransfer.TransferAmount =
            Validator.Convert<decimal>($"amount {cur}");
        internalTransfer.RecipientBankAccountName =
            Utility.GetUserInput("recipient name: ");
        return internalTransfer;
    }

}