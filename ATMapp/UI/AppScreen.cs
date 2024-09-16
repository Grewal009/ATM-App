namespace ATMapp.UI;

public static class AppScreen
{
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

    
}