// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

Word firstWord = new Word(@"Indianabaa", 0.0, 0.0);
Word secondWord = new Word(@"Mississipi", 0.0, 0.0);

WordComparison result = firstWord.GuessWord(secondWord);

Console.WriteLine("Done!");
string fiveLetterWordsFilepath = @".\sgb-words.txt";

/*
using (WordComparisonContext context = new WordComparisonContext())
{
    
    using (StreamReader sr = new StreamReader(fiveLetterWordsFilepath))
    {
        context.Words.Add(new Word(sr.ReadLine()));
    }
}
*/
List<Word> wordList = new List<Word>();
string? currentLine;
List<string> strings = new List<string>();
using (StreamReader sr = new StreamReader(fiveLetterWordsFilepath))
{
    do
    {
        currentLine = sr.ReadLine();
        if (currentLine != null)
            strings.Add(currentLine);
        else
            break;
    } while (sr.EndOfStream == false);
}
int numRecords = strings.Count;
for (int i = 0; i < strings.Count; i++)
{
    wordList.Add(new Word(strings[i], 1.0 / (strings.Count + 1), i * 1.0 / (strings.Count + 1)));
}


WordleAI ai = new WordleAI(wordList);
Random rand = new Random();
Word wordToGuess = wordList[rand.Next(wordList.Count)];
ai.Solve(wordToGuess);


