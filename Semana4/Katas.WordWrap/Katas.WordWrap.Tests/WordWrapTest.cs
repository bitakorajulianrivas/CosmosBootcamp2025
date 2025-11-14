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
    
    [Fact]
    public void c()
    {
        var result = Wrap("word", 2);

        result.Should().Be("wo\nrd");
    } 
    
    private static string Wrap(string text, int col)
    {
        if (col == 1)
            return text.Substring(0);
        
        if (text.Length < col)
            return text.Substring(0, text.Length);
        
        if (col == 2)
            return text.Substring(0, 2) + '\n' + text.Substring(2, 2);
        
        throw new Exception();
    }
}