// See https://aka.ms/new-console-template for more information

internal class SolverAttempt
{
    public Word WordToSolve { get; }
    public List<Word> WordList { get;  }
    public List<List<double>> WordWeights { get; }
    public int NumberOfGuesses { get;  }

    public SolverAttempt(Word wordToSolve, List<Word> wordList, List<List<double>> wordWeights, int numberOfGuesses)
    {
        this.WordToSolve = wordToSolve;
        this.WordList = wordList;
        this.WordWeights = wordWeights;
        this.NumberOfGuesses = numberOfGuesses;
    }
}