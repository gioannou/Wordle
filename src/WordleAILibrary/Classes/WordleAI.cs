// See https://aka.ms/new-console-template for more information

using System;

public class WordleAI
{
    private List<Word> _wordList;
    private List<Guess> _guesses;
    
    private Random _random;
    private Word _currentGuess;
    private Word _wordToSolve;

    int _numberOfGuesses;

    public WordleAI(List<Word> wordList)
    {
        _wordList = new List<Word>(wordList);
        _random = new Random();
        _guesses = new List<Guess>();
        
    }
    public SolverAttempt Solve(Word wordToSolve)
    {
        _wordToSolve = wordToSolve;
        List<WordComparison> comparisons = new List<WordComparison>();
        int numberOfGuesses = 1;
        List<Weight> currentWordWeights = new List<Weight>();
        double randomWeight = 0.0;
        double closestWeight = 0.0;
        List<Word> workingWordList = new List<Word>(_wordList);
        WordComparison currentComparison;
        do
        {
            foreach (Word word in workingWordList)
            {
                if (word.Weights.Count() < numberOfGuesses)
                {
                    currentWordWeights = new List<Weight>();
                    for (int i = 0; i < workingWordList.Count; i++)
                    {
                        currentWordWeights.Add(new Weight(1.0 / (_wordList.Count + 1), i * 1.0 / (_wordList.Count + 1)));
                    }
                    word.Weights.Add(currentWordWeights);
                }
            }
            currentWordWeights = workingWordList.Select(x => x.Weights[numberOfGuesses]).ToList();
            randomWeight = _random.NextDouble();
            closestWeight = currentWordWeights.SkipWhile(x => x <= randomWeight).First();            
            _currentGuess = workingWordList[currentWordWeights.IndexOf(closestWeight)];
            currentComparison = _wordToSolve.GuessWord(_currentGuess);
            comparisons.Add(currentComparison);
            workingWordList = ReturnPossibleWordList(workingWordList, comparisons);
            numberOfGuesses++;
        } while (currentComparison.ExactMatches.Count() != _wordToSolve.Value.Length);


        return new SolverAttempt(_wordToSolve, _wordList, _wordWeights, numberOfGuesses);    
    
    }

    private List<Word> ReturnPossibleWordList(List<Word> wordList, List<WordComparison> comparisons)
    {
        List<char> excludedLetters;
        List<char> includedLetters;
        List<Word> retval = new List<Word>();

        excludedLetters = comparisons
            .SelectMany(x => x.NoMatches.Select(y => y.Letter))
            .ToList();
        includedLetters = comparisons
            .SelectMany(x => x.ExactMatches.Select(y => y.Letter))
            .Union( comparisons
                .SelectMany(x => x.CloseMatches.Select(y => y.Letter)))
            .ToList();

        ///check the excluded letters from previous guesses. all words that contain the exluded letters are no longer possible
        List <Word> shortList = wordList
            .Where(x => !x.Value.Intersect(excludedLetters).Any())
            .Where(x => x.Value.Intersect(includedLetters).Count() == includedLetters.Count)
            .ToList();
        
        foreach (var word in shortList)
        {
            //foreach (var comparison in comparisons)
            //{
            //    var currentGuess = word.GuessWord(comparison.GuessedWord);

            //    ///check the exact matches. all of the exact matches from the previous guesses should be exact matches on the new word
            //    if (!comparison.ExactMatches.All(x => currentGuess.ExactMatches.Any(y => x.Index == y.Index && x.Letter == y.Letter)))
            //    {
            //        break;
            //    }
            //    ///check the close matches. all of the close matches from the previous guesses should be close matches on the new word (different index)
            //    if (!comparison.CloseMatches.All(x => currentGuess.CloseMatches.Any(y => x.Index != y.Index && x.Letter == y.Letter)))
            //    {
            //        break;
            //    }
            //}

            var x = comparisons
                .Select(comparison => new { comparison, currentGuess = comparison.GuessedWord.GuessWord(word) })
                .ToList();

            var y = x
                .Where(x => x.comparison.ExactMatches.All(z => x.currentGuess.ExactMatches.Any(y => z.Index == y.Index && z.Letter == y.Letter)))
                .ToList(); 

            var z = y
                .Where(x => x.comparison.CloseMatches.All(z => x.currentGuess.CloseMatches.Any(y => z.Index != y.Index && z.Letter == y.Letter)))
                .ToList();


            if (comparisons
                .Select(comparison => new { comparison, currentGuess = comparison.GuessedWord.GuessWord(word) })
                .All(x => x.comparison.ExactMatches.All(z => x.currentGuess.ExactMatches.Any(y => z.Index == y.Index && z.Letter == y.Letter))
                        &&
                          x.comparison.CloseMatches.All(z => x.currentGuess.CloseMatches.Any(y => z.Index != y.Index && z.Letter == y.Letter))))
            {
                retval.Add(word);
            }
        }
        return retval;
    }
}
