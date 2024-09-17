using ATMapp.Domain.Entities;
using ATMapp.Domain.Enums;
using ATMapp.Domain.Interfaces;
using ATMapp.UI;
using static ATMapp.Domain.Enums.AppMenu;

namespace ATMapp.App;

public class ATMApp : IUserLogin
{
    private List<UserAccount> userAccountList;

    private UserAccount selectedAccount;

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
                Console.WriteLine("checking account balance...");
                break;
            case (int)AppMenu.PlaceDeposits:
                Console.WriteLine("placing deposits...");
                break;
            case (int)AppMenu.MakeWithdrawal:
                Console.WriteLine("making withdrawal...");
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


}