// See https://aka.ms/new-console-template for more information
internal class ExactMatch
{
    public int ExactMatchId { get; }
    public int Index { get; }
    public char Letter { get; }

    public ExactMatch  (int index, char letter)
    {
        Index = index;
        Letter = letter;
    }
}