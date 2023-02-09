//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Utilities;

public static class CommandUtilities
{
    public static bool TryExecuteUserInput(string userInput, List<Command> availableCommands)
    {
        userInput = userInput.ToLower();

        //try to find global command in user input
        foreach (Command command in Game.GlobalCommands)
        {
            if (!command.ContainsKeyword(userInput)) continue;

            //remove the command keywords
            //string userInputWithoutCommand = command.RemoveCommandKeyword(userInput);
            //find and split parameters
            List<string> userInputSplitIntoWords = userInput.SplitIntoWords();
            
            //execute command
            command.Execute(userInputSplitIntoWords);
            return true;
        }
        
        //try to find non-global command in user input
        foreach (Command command in availableCommands)
        {
            if (!command.ContainsKeyword(userInput)) continue;

            //remove the command keywords
            //string userInputWithoutCommand = command.RemoveCommandKeyword(userInput);
            //find and split parameters
            List<string> userInputSplitIntoWords = userInput.SplitIntoWords();
            
            //execute command
            command.Execute(userInputSplitIntoWords);
            return true;
        }

        return false;
    }

    public static void ListAvailableCommands(List<Command> availableCommands)
    {
        Console.WriteLine("You can perform the following actions: \n");

        foreach (Command command in Game.GlobalCommands)
        {
            if(command.Hidden) continue;
            Console.WriteLine($"- {command.Name}");
        }
        
        foreach (Command command in availableCommands)
        {
            if(command.Hidden) continue;
            Console.WriteLine($"- {command.Name}");
        }
    }

    public static void ListAvailableDoors(Room room)
    {
        Console.WriteLine($"The room you're in has {room.LinkedDoors.Count} {(room.LinkedDoors.Count > 1 ? "doors" : "door")}.");

        string firstDoor = (room.LinkedDoors.Count == 1) ? "The only door in this room leads to the " : "The first door leads to the ";
        string secondDoor = "Another door leads to the ";
        string thirdDoor = "The third door leads to the ";
        string fourthDoor = "The last door leads to the ";

        var doorSentences = new List<string> { firstDoor, secondDoor, thirdDoor, fourthDoor };

        int doorCount = 0;

        if (room.HasDoorAt(Direction.North))
        {
            Console.WriteLine(doorSentences[doorCount] + "north.");
            doorCount += 1;
        }

        if (room.HasDoorAt(Direction.East))
        {
            Console.WriteLine(doorSentences[doorCount] + "east.");
            doorCount += 1;
        }

        if (room.HasDoorAt(Direction.South))
        {
            Console.WriteLine(doorSentences[doorCount] + "south.");
            doorCount += 1;
        }

        if (room.HasDoorAt(Direction.West))
        {
            Console.WriteLine(doorSentences[doorCount] + "west.");
            doorCount += 1;
        }
    }
}