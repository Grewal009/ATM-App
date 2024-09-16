using ATMapp.UI;

AppScreen.Welcome();
long cardNumber = Validator.Convert<long>("card number");
Console.WriteLine($"your card number is {cardNumber}");

Utility.PressEnterToContinue();

