using ATMapp.App;
using ATMapp.UI;

AppScreen.Welcome();

ATMApp atmApp = new ATMApp();
atmApp.CheckUserCardNumberAndPassword();

Utility.PressEnterToContinue();

