using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

class Program
{
    static bool IsVowel(char c)
    {
        char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };
        return vowels.Contains(c);
    }

    static void Main(string[]? args)
    {
        string filePath = "/Users/nataliiagricisin/Documents/3 курс/II семестр/lab1_jUnit/lab1_jUnit/input.txt";

        try
        {
            string[] words = File.ReadAllText(filePath)
                .Split(new char[] { ' ', '\t', '\n', '\r', ',', '.', '!', '?', '|', '(', ')', '$' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(word => Regex.Replace(word, "[^a-zA-Z]", ""))
                .Where(word => !string.IsNullOrEmpty(word))
                .Where(word => word.All(IsVowel))
                .Distinct()
                .ToArray();

            foreach (string word in words)
            {
                Console.WriteLine(word);
            }
        }
        catch (IOException e)
        {
            Console.WriteLine("Unable to read the file: " + e.Message);
        }
    }
}
