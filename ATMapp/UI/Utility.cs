using System.Text;

namespace ATMapp.UI;

public static class Utility
{
    
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
                Console.WriteLine(prompt);
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
        Console.WriteLine("Press enter to continue...");
        Console.ReadLine();
    }

    public static string GetUserInput(string prompt)
    {
        Console.WriteLine($"Enter {prompt}");
        return Console.ReadLine();
    }
}