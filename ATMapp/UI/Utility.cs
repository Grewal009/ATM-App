namespace ATMapp.UI;

public static class Utility
{
    public static void PressEnterToContinue()
    {
        Console.WriteLine("\nPress enter to continue...\n");
        Console.ReadLine();
    }

    public static string GetUserInput(string prompt)
    {
        Console.WriteLine($"Enter {prompt}");
        return Console.ReadLine();
    }
}