// See https://aka.ms/new-console-template for more information
internal class CloseMatch
{
    public int CloseMatchId { get; }
    public int Index { get; }
    public char Letter { get; }

    public CloseMatch(int index, char letter)
    {
        Index = index;
        Letter = letter;
    }
}