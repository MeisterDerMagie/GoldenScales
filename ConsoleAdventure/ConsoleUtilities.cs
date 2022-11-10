//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.ConsoleUtilities;

public static class ConsoleUtilities
{
    private static bool InputBoolean(string _question)
    {
        while (true)
        {
            Console.Write(_question);
            Console.Write(" [yes/no]? ");

            string userInput = Console.ReadLine();

            if (string.IsNullOrEmpty(userInput))
                continue;

            userInput = userInput.ToLower();
            
            if ((string.CompareOrdinal(userInput, "n") == 0)
                || (string.CompareOrdinal(userInput, "no") == 0))
            {
                return false;
            }
            
            if ((string.CompareOrdinal(userInput, "y") == 0)
                || (string.CompareOrdinal(userInput, "yes") == 0))
            {
                return true;
            }
        }
    }

    private static string InputString(string _question)
    {
        while (true)
        {
            Console.Write(_question);
            Console.Write(" ");

            string userInput = Console.ReadLine();

            if (string.IsNullOrEmpty(userInput))
                continue;

            return userInput;
        }
    }

    private static int InputInteger(string _question, bool _positiveOnly = true, bool _allowZero = true)
    {
        while (true)
        {
            Console.Write(_question);

            string userInput = Console.ReadLine();

            if (string.IsNullOrEmpty(userInput))
                continue;

            if (!int.TryParse(userInput, out int ret))
            {
                Console.WriteLine("Please enter a valid integer.");
                continue;
            }

            if (ret == 0)
            {
                if (_allowZero)
                {
                    return 0;
                }
                else
                {
                    Console.WriteLine("Zero is not allowed.");
                    continue;
                }
            }

            if (ret < 0)
            {
                if (_positiveOnly)
                {
                    Console.WriteLine("Please enter a positive value.");
                    continue;
                }
            }

            return ret;
        }
    }
}