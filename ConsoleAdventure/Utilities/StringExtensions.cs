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

    
    public static string RemoveFirstOccurence(this string sourceString, string removeString)
    {
        int index = sourceString.IndexOf(removeString, StringComparison.Ordinal);
        string stringWithoutFirstOccurenceOfSubstring = (index < 0)
            ? sourceString
            : sourceString.Remove(index, removeString.Length);
        
        return stringWithoutFirstOccurenceOfSubstring.RemoveDuplicateWhiteSpaces();
    }

    //https://stackoverflow.com/a/206724/13174465
    public static string RemoveDuplicateWhiteSpaces(this string sourceString)
    {
        //also removes leading and trailing spaces
        string cleanString = string.Join( " ", sourceString.Split( new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries ));
        return cleanString;
    }
}