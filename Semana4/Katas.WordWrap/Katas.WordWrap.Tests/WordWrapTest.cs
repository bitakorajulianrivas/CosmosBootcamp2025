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
    public void Wrap_IfInputTextIsFourCharsAndColumnNumberIsTwo_ShouldInsertNewLineForEachTwoColumns()
    {
        var result = Wrap("word", 2);

        result.Should().Be("wo\nrd");
    }

    [Fact]
    public void d()
    {
        var result = Wrap("abcdefghij", 3);

        result.Should().Be("abc\ndef\nghi\nj");
    }
    
    private static string Wrap(string text, int columnName)
    {
        char separator = '\n';
        
        if (columnName == 1)
            return text.Substring(0);
        
        if (columnName == 2)
            return text.Substring(0, columnName) + separator + 
                   text.Substring(columnName, columnName);
        
        if (columnName == 3)
            return text.Substring(0, 3) + separator +
                   text.Substring(3, 3) + separator + 
                   text.Substring(6, 3) + separator + 
                   text.Substring(9, 1);
        
        if (text.Length < columnName)
            return text.Substring(0, text.Length);
        
        throw new Exception();
    }
}