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
        throw new Exception();
    }
}