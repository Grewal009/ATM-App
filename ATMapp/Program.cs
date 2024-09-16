using ATMapp.App;
using ATMapp.UI;

AppScreen.Welcome();
ATMApp app = new ATMApp();
app.InitializeData();
app.CheckUserCardNumberAndPassword();
app.Welcome();

Utility.PressEnterToContinue();


