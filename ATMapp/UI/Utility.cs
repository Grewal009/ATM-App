using System.Globalization;
using System.Text;

namespace ATMapp.UI;

public static class Utility
{
    private static long transId;
    
    private static CultureInfo culture = new CultureInfo("nb-NO");
    
    public static long GetTransactionId()
    {
        return ++transId;
    }
    internal static void PrintDotAnimation(int timer = 10)
    {
        for (int i = 0; i < timer; i++)
        {
            Console.Write(".");
            Thread.Sleep(300);
        }
        Console.Clear();
    }
    public static string GetSecretInput(string prompt)
    {
        bool isPrompt = true;
        string asterics = "";

        StringBuilder input = new StringBuilder();

        while (true)
        {
            if (isPrompt)
                Console.Write(prompt);
            isPrompt = false;
            
            ConsoleKeyInfo inputKey = Console.ReadKey(true);

            if (inputKey.Key == ConsoleKey.Enter)
            {
                if (input.Length == 6)
                {
                    break;
                }
                else
                {
                    PrintMessage("\nplease enter 6 digits", false);
                    input.Clear();
                    isPrompt = true;
                    continue;
                }
            }

            if (inputKey.Key == ConsoleKey.Backspace && input.Length > 0)
            {
                input.Remove(input.Length - 1, 1);
            }
            else if(inputKey.Key != ConsoleKey.Backspace)
            {
                input.Append(inputKey.KeyChar);
                Console.Write(asterics + "*");
            }

        }
        return input.ToString();
    }

    public static void PrintMessage(string msg, bool success=true)
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
        Console.WriteLine("Press enter to continue...");
        Console.ReadLine();
    }

    public static string GetUserInput(string prompt)
    {
        Console.Write($"Enter {prompt}: ");
        return Console.ReadLine();
    }

    public static string FormatAmount(decimal amt)
    {
        
        return string.Format(culture, "{0:C2}", amt); //three parameters ->currency symbol, desired no of decimal places in the result string, amt stands for actual amount
    }

}