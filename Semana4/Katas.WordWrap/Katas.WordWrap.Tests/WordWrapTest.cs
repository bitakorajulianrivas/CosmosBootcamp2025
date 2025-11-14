using FluentAssertions;

namespace WordWrap.Tests;

public class WordWrapTest
{
    [Fact]
    public void Wrap_IfInputTextIsEmptyAndColumnNumberIsOne_ShouldReturnEmptyString()
    {
        var result = Wrap("", 1);

        result.Should().Be("");
    }

    [Fact]
    public void Wrap_IfInputTextLengthIsLessThanColumnNumber_ShouldReturnTheSameString()
    {
        var result = Wrap("this", 10);

        result.Should().Be("this");
    }  
    
    private static string Wrap(string text, int col)
    {
        if (col == 1)
            return text.Substring(0);
        
        if (text.Length < col)
            return text.Substring(0, text.Length);
        
        throw new Exception();
    }
}