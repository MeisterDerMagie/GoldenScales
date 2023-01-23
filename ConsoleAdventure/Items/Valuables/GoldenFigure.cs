//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Items.Valuables;

public class GoldenFigure : Item
{
    public GoldenFigure(int value, List<Command> interactions) : base(false, false, value, interactions, "Golden Figure")
    {
        
    }

    public override void Interact(string userInput)
    {
        throw new NotImplementedException();
    }

    public override void Examine()
    {
        Console.WriteLine($"You examine the {Name}. It's a small statue made out of pure gold. It seems to be very valuable but has no other use. You should search for someone who is interested in it.");
    }
}