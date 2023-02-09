//(c) copyright by Martin M. Klöckener
// ReSharper disable InconsistentNaming
namespace ConsoleAdventure.NPCs;

public class DialogFactory
{
    public static DialogNode GetTraderDialog(Trader trader)
    {
        var dialog = new DialogNode(
            string.Empty, 
            $"Welcome, my good fellow! I am {trader.Name}, proud member of the Merchants Guild. What can I serve you with?", 
            null);
        DialogNode node_1_1 = dialog.AddChild( 
            "What are you doing down here? I can hardly imagine that many customers come by here. Wouldn't you be better off selling your goods at the market in town?", 
            "Well, you're down here too. You wouldn't believe how many \"adventurers\" are hot for the treasure of the lindworm. And so far, no one has been able to defeat it. That means there's a lot of valuables accumulating down here over time, if you know what I mean.... The business is filthier than a market stall, but all the more rewarding because there is less competition.");
        DialogNode node_1_2 = dialog.AddChild(
            "Let's get straight to the business. I want to trade with you.",
            "That's the way I like it. Here are my goods.",
            "Trade",
            () => Game.StateMachine.SetState(new Trade(trader)));
        DialogNode node_1_3 = dialog.AddChild(
            "Enough talk. I'm going to continue exploring the dungeon. See you around.",
            "Alright. You know where to find me if you want to get rid of some gold coins.",
            "Leave",
            () => Game.StateMachine.SetState(Game.ExplorationState, false));
        DialogNode node_1_1_1 = node_1_1.AddChild(
            "Isn't it depressing to spend all day down here among rats, mold and death?",
            "You get used to it. At least I find it more bearable than being annoyed by gossip at the market.");
        DialogNode node_1_1_1_1 = node_1_1_1.AddChild(
            "I won't stay here a second longer than necessary. I'll be glad when I've finally looted the treasure here and can spend the rest of my life lying on my lazy skin. The sooner I get out of this hellhole, the better.",
            "Haha, that's what they all say. And then they get hit in the head by a skeleton and end up as a rotten pile of bones. Nah, I earn my modest money here. I won't be doing any more great deeds, and that's a good thing. Speaking of earning money: Are you here to chat or to trade?");
        DialogNode node_1_1_1_2 = node_1_1_1.AddChild(
            "My opinion exactly! I prefer a good adventure on my own over an afternoon on the couch with gossip and cake.",
            "We gossip about gossip. Let's trade instead!");
        DialogNode node_1_1_1_2_1 = node_1_1_1_2.AddChild(
            "Yes, yes, all right. Show me your goods.",
            "With pleasure!",
            "Trade",
            () => Game.StateMachine.SetState(new Trade(trader)));
        DialogNode node_1_1_1_2_2 = node_1_1_1_2.AddChild(
            "No thank you, I dont't want to trade right now. Good bye!",
            "Too bad! Be careful down here.",
            enterAction: () => Game.StateMachine.SetState(Game.ExplorationState, false));
        DialogNode node_1_1_1_1_1 = node_1_1_1_1.AddChild(
            "Yes, yes, all right. Show me your goods.",
            "With pleasure!",
            "Trade",
            () => Game.StateMachine.SetState(new Trade(trader)));
        DialogNode node_1_1_1_1_2 = node_1_1_1_1.AddChild(
            "No thank you, I dont't want to trade right now. Good bye!",
            "Too bad! Be careful down here.",
            "Leave",
            () => Game.StateMachine.SetState(Game.ExplorationState, false));
        DialogNode node_1_1_2 = node_1_1.AddChild(
            "How do you actually get to work in the morning? Your way to work is full of skeletons, spiders and traps.",
            "An old friend of mine has a store in the Mage Quarter. There I get some useful items that help me. Invisibility potions and such. I can recommend it to you!");
        DialogNode node_1_1_2_1 = node_1_1_2.AddChild(
            "Invisibility Potion? Why don't you just steal the Lindworm's treasure and lay low for the rest of your life?",
            "Ha, if it were that easy! I would have been lying in the sun on the Azur Islands long ago. The lindworm notices every little movement, so an invisibility potion isn't of much use. Believe me, I've tried. I survived the attempt by a hair's breadth.");
        DialogNode node_1_1_2_1_1 = node_1_1_2_1.AddChild(
            "Phew, lucky! Were you able to steal at least a few things from the treasure? Let me take a look at your goods!",
            "Indeed. I was able to take one or two valuable pieces with me. Have a look at them!",
            "Trade",
            () => Game.StateMachine.SetState(new Trade(trader)));
        DialogNode node_1_1_2_1_2 = node_1_1_2_1.AddChild(
            "All right, thanks for the warning! I will be careful.",
            "Good luck and take care!",
            "Leave",
            () => Game.StateMachine.SetState(Game.ExplorationState));

        return dialog;
    }
}