namespace ConsoleAdventure.Items;

public abstract class Armor : Item
{
    public int Protection { get; }
    public BodyPart BodyPart;

    protected Armor(int value, List<Command> interactions, string name, int protection, BodyPart bodyPart) : base(true, false, value, interactions, name)
    {
        Protection = protection;
        BodyPart = bodyPart;
    }
}