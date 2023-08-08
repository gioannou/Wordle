// See https://aka.ms/new-console-template for more information
using System.Text.RegularExpressions;

internal class WordComparison
{
    public Word GuessedWord { get; }
    public int NumExactMatches { get; }
    public int NumCloseMatches { get; }
    public int NumNoMatches { get; }
    public virtual List<ExactMatch> ExactMatches { get; }
    public virtual List<CloseMatch> CloseMatches { get; }
    public virtual List<NoMatch> NoMatches { get; }
    public WordComparison(Word wordGuessed, int numExactMatches, int numCloseMatches, int numNoMatches, List<ExactMatch> exactMatches, List<CloseMatch> closeMatches, List<NoMatch> noMatches)
    { 
        GuessedWord = wordGuessed;
        NumExactMatches = numExactMatches;
        NumCloseMatches = numCloseMatches;
        NumNoMatches = numNoMatches;
        ExactMatches = exactMatches;
        CloseMatches = closeMatches;
        NoMatches = noMatches;    
    }    
}