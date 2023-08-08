// See https://aka.ms/new-console-template for more information
internal class NoMatch
{
    public int NoMatchId { get; }
    public int Index { get; }
    public char Letter { get; }
    public NoMatch(int index, char letter)
    {
        Index = index;
        Letter = letter;
    }
}