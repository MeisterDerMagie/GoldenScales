//(c) copyright by Martin M. Klöckener

namespace ConsoleAdventure.Utilities;

public static class StringExtensions
{
    //https://stackoverflow.com/questions/16725848/how-to-split-text-into-words
    public static List<string> SplitIntoWords(this string input)
    {
        char[] punctuation = input.Where(Char.IsPunctuation).Distinct().ToArray();
        List<string> words = input.Split().Select(x => x.Trim(punctuation)).ToList();

        return words;
    }
}