using ATMapp.Domain.Entities;

namespace ATMapp.App;

public class ATMApp
{
    List<UserAccount> userAccountList = new List<UserAccount>
    {
        new UserAccount(1,123456,123123,123123123,"Per",10000.00m,0,false),
        new UserAccount(2,223344,123123,123123124,"Pål",15000.50m,0,false),
        new UserAccount(3,334455,123000,123123125,"Peter",20000.90m,0,false),
    };
    UserAccount selectedAccount;
}