using Microsoft.VisualStudio.TestPlatform.TestHost;
using NUnit.Framework;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

[TestFixture]
public class ProgramTests
{
    private string[] words;

    [OneTimeSetUp] // аналог @BeforeClass або @BeforeAll
    public void OneTimeSetup()
    {
        string filePath = "/Users/nataliiagricisin/Documents/3 курс/II семестр/lab1_jUnit/lab1_jUnit/input.txt";
        words = File.ReadAllText(filePath)
            .Split(new char[] { ' ', '\t', '\n', '\r', ',', '.', '!', '?', '|', '(', ')', '$' }, StringSplitOptions.RemoveEmptyEntries)
            .Select(word => Regex.Replace(word, "[^a-zA-Z]", ""))
            .Where(word => !string.IsNullOrEmpty(word))
            .Distinct()
            .ToArray();
    }

    [SetUp] // аналог @Before або @BeforeEach
    public void Setup()
    {
        // Додаткові налаштування перед кожним тестом, якщо потрібно
    }

    [Test]
    public void TestVowelWords()
    {
        // Arrange (підготовка)
        var expectedWords = words.Where(word => word.All(IsVowel)).Distinct().ToArray();

        // Act (дія)
        var actualWords = GetVowelWords();

        // Assert (перевірка)
        Assert.AreEqual(expectedWords, actualWords);
    }

    [Test]
    public void TestNonVowelWords()
    {
        // Arrange (підготовка)
        var expectedWords = words.Where(word => !word.All(IsVowel)).Distinct().ToArray();

        // Act (дія)
        var actualWords = GetNonVowelWords();

        // Assert (перевірка)
        Assert.AreEqual(expectedWords, actualWords);
    }

    private string[] GetVowelWords()
    {
        return words.Where(word => word.All(IsVowel)).Distinct().ToArray();
    }

    private string[] GetNonVowelWords()
    {
        return words.Where(word => !word.All(IsVowel)).Distinct().ToArray();
    }

    private bool IsVowel(char c)
    {
        char[] vowels = { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };
        return vowels.Contains(c);
    }

    [Test]
    public void TestUniqueVowelWordsCount()
    {
        // Arrange (підготовка)
        var expectedCount = GetUniqueVowelWords().Length; // Очікуємо кількість унікальних голосних слів, які фактично є в файлі

        // Act (дія)
        var actualCount = GetUniqueVowelWords().Length;

        // Assert (перевірка)
        Assert.AreEqual(expectedCount, actualCount);
    }

    private string[] GetUniqueVowelWords()
    {
        return GetVowelWords().Distinct().ToArray();
    }

    [Test]
    public void TestNonVowelWordsExistence()
    {
        // Arrange (підготовка)
        var nonVowelWords = new string[] { "apple", "banana" }; // Припустимо, що ці слова є в файлі

        // Act (дія)
        var actualNonVowelWords = GetNonVowelWords();

        // Assert (перевірка)
        CollectionAssert.IsSubsetOf(nonVowelWords, actualNonVowelWords);
    }

    [Test]
    public void TestAllWordsDistinct()
    {
        // Arrange (підготовка)
        var expectedDistinctCount = words.Length;

        // Act (дія)
        var actualDistinctCount = words.Distinct().Count();

        // Assert (перевірка)
        Assert.AreEqual(expectedDistinctCount, actualDistinctCount);
    }

    [Test]
    public void TestFileExtension()
    {
        // Arrange (підготовка)
        string filePath = "/Users/nataliiagricisin/Documents/3 курс/II семестр/lab1_jUnit/lab1_jUnit/input.txt";
        var fileInfo = new FileInfo(filePath);

        // Act (дія)
        var fileExtension = fileInfo.Extension;

        // Assert (перевірка)
        Assert.That(fileExtension, Is.EqualTo(".txt"));
    }

    [Test]
    public void TestAllCharactersAreVowels()
    {
        // Arrange (підготовка)
        string word = "aieou"; // Припустимо, що це унікальне слово, яке складається з голосних

        // Act (дія)
        bool allCharactersAreVowels = AllCharactersAreVowels(word);

        // Assert (перевірка)
        Assert.IsTrue(allCharactersAreVowels, "All characters should be vowels");
    }

    private bool AllCharactersAreVowels(string word)
    {
        return word.All(IsVowel);
    }


    [Test]
    public void TestNullReferenceException()
    {
        // Arrange (підготовка)
        object obj = null;

        // Act (дія та перевірка винятку)
        var exception = Assert.Throws<NullReferenceException>(() => obj.ToString());

        // Assert (перевірка)
        Assert.IsNotNull(exception, "A NullReferenceException should be thrown");
    }


    [Test]
    public void TestWordCount()
    {
        // Arrange (підготовка)
        var expectedCount = 11; // Очікуємо 11 слів у файлі

        // Act (дія)
        var actualCount = words.Length;

        // Assert (перевірка)
        Assert.That(actualCount, Is.EqualTo(expectedCount), "Word count should match the expected value");
    }

    [Test]
    public void TestSpecificWordsExistence()
    {
        // Arrange (підготовка)
        var expectedWords = new string[] { "apple", "banana" }; // Очікуємо, що ці слова є у файлі

        // Act (дія)
        var actualWords = words;

        // Assert (перевірка)
        CollectionAssert.IsSubsetOf(expectedWords, actualWords, "Expected words should exist in the list of words");
    }

    [TestCaseSource(nameof(GetVowelWordsSource))]
    public void TestIsVowel(string word)
    {
        // Arrange
        bool expected = word.All(IsVowel);

        // Act
        bool actual = IsVowel(word[0]);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    private static IEnumerable<string> GetVowelWordsSource()
    {
        yield return "aeiou";
        yield return "AEIOU";
        yield return "hello";
        yield return "world";
    }

}
