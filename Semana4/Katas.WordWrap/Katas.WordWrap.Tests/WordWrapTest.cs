using FluentAssertions;

namespace WordWrap.Tests;

public class WordWrapTest
{
    [Fact]
    public void a()
    {
        var result = Wrap("", 1);

        result.Should().Be("");
    }
    
    private static string Wrap(string text, int col)
    {
        if (col == 1)
            return string.Empty;
        
        throw new Exception();
    }
}