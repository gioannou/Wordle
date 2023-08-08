// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

internal class Word
{
    public int WordId { get; }
    public string Value { get; }
    public List<Weight> Weights { get; }
    public bool Active { get { return _active; }  }
    private bool _active;
        
    public List<WordComparison> Comparisons { get; }
    public Word(string value, double initialWeight, double initialCumulativeWeight)
    {
        if (value == null) throw new Exception("Words cannot be null!");

        if (Regex.Match(value, @"[^\p{L}]").Success) throw new Exception("Words must contain only letters!");

        this.Value = value;

        Comparisons = new List<WordComparison>();
        Weights = new List<Weight>();
        Weights.Add(new Weight(initialWeight, initialCumulativeWeight));
        _active = true;
    }
    public void Enable()
    {
        _active = true;
    }
    public void Disable()
    {
        _active = false;
    }

    public override string ToString()
    {
        return Value; 
    }

    public WordComparison GuessWord(Word currentGuess)
    {
        if (this.Value.Length != currentGuess.Value.Length) throw new Exception("Input words cannot have different lengths!");

        int numExactMatches = 0;
        int numCloseMatches = 0;
        int numNoMatches = 0;

        List<ExactMatch> exactMatches = new List<ExactMatch>();
        List<CloseMatch> closeMatches = new List<CloseMatch>();
        List<NoMatch> noMatches = new List<NoMatch>();

        for (int i = 0; i < this.Value.Length; i++)
        {
            if (this.Value[i] == currentGuess.Value[i])
            {
                exactMatches.Add(new ExactMatch(i, currentGuess.Value[i]));
                numExactMatches++;
            }
        }
        for (int j = 0; j < currentGuess.Value.Length; j++)
        {
            for (int i = 0; i < this.Value.Length; i++)
            {
                if (i != j)
                {
                    if (!exactMatches.Any(match => match.Index == i || match.Index == j)) //check if exactMatches has picked up either of these letters
                    {
                        if (!closeMatches.Any(match => match.Index == j)) //check if closeMatches has pickedup any of these letters
                        {
                            if (this.Value[i] == currentGuess.Value[j])
                            {
                                closeMatches.Add(new CloseMatch(j, currentGuess.Value[j]));
                                numCloseMatches++;
                                break;
                            }
                        }
                    }                    
                }                
            }
        }

        for (int k = 0; k < currentGuess.Value.Length; k++)
        {
            if (!this.Value.Contains(currentGuess.Value[k]))
            {
                noMatches.Add(new NoMatch(k, currentGuess.Value[k]));
                numNoMatches++;
            }
        }

        WordComparison comp = new WordComparison(currentGuess, numExactMatches, numCloseMatches, numNoMatches, exactMatches, closeMatches, noMatches);
        Comparisons.Add(comp);
        return comp;
    }
}



