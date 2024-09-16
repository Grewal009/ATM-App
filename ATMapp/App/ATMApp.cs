using ATMapp.Domain.Entities;
using ATMapp.Domain.Interfaces;
using ATMapp.UI;

namespace ATMapp.App;

public class ATMApp : IUserLogin
{
    List<UserAccount> userAccountList = new List<UserAccount>
    {
        new UserAccount(1,123456,123123,123123123,"Per",10000.00m,0,false),
        new UserAccount(2,223344,123123,123123124,"Pål",15000.50m,0,false),
        new UserAccount(3,334455,123000,123123125,"Peter",20000.90m,0,false),
    };
    UserAccount selectedAccount;
    public void CheckUserCardNumberAndPassword()
    {
        bool IsCorrectLogin = false;

        UserAccount tempUserAccount = new UserAccount();

        tempUserAccount.CardNumber = Validator.Convert<long>("card number");
        tempUserAccount.CardPin = Convert.ToInt32(Utility.GetSecretInput("enter your card PIN"));

    }
}