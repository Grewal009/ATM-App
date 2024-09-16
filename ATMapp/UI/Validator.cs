using System.ComponentModel;

namespace ATMapp.UI;

public static class Validator
{
    public static T Convert<T>(string prompt)
    {
        bool valid = false;
        string userInput;

        while (!valid)
        {
            userInput = Utility.GetUserInput(prompt);

            try
            {
                var converter = TypeDescriptor.GetConverter(typeof(T));
                if (converter != null)
                {
                    return (T)converter.ConvertFromString(userInput);
                }
                else
                {
                    return default;
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                //Console.WriteLine("invalid input. try again.");
                //throw;
                Utility.PrintMessage("Invalid input. try again", false);
            }
            
        }

        return default;
    }
}