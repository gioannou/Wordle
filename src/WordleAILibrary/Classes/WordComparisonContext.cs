// See https://aka.ms/new-console-template for more information
using Microsoft.EntityFrameworkCore;

class WordComparisonContext : DbContext
{ 
    public DbSet<Word> Words { get; set; }
    public DbSet<WordComparison> WordComparisons { get; set; }

}
