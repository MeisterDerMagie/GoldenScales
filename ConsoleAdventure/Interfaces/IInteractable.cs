//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure;

public interface IInteractable
{
    public string Keyword { get; }
    public string[] Interactions { get; }

    public void Interact(string userInput);
}