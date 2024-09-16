namespace ATMapp.UI;

public static class Utility
{
    public static void PrintMessage(string msg, bool success)
    {
        if (success)
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        Console.WriteLine(msg);
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        PressEnterToContinue();
    }

    public static void PressEnterToContinue()
    {
        Console.WriteLine("\nPress enter to continue...");
        Console.ReadLine();
    }

    public static string GetUserInput(string prompt)
    {
        Console.WriteLine($"Enter {prompt}");
        return Console.ReadLine();
    }
}