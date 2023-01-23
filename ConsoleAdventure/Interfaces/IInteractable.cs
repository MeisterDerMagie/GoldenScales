//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure;

public interface IInteractable
{
    public string Keyword { get; }
    public List<Command> Interactions { get; }

    public void Interact(string userInput);
}