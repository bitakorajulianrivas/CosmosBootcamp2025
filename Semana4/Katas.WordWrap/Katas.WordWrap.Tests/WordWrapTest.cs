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
    public void Wrap_IfInputTextHasFourCharsAndColumnNumberIsTwo_ShouldInsertNewLineForEachTwoColumns()
    {
        var result = Wrap("word", 2);

        result.Should().Be("wo\nrd");
    }

    [Fact]
    public void Wrap_IfInputTextHasTenCharsAndColumnNumerIsThree_ShouldInsertNewLineForEachThreeColumns()
    {
        var result = Wrap("abcdefghij", 3);

        result.Should().Be("abc\ndef\nghi\nj");
    }

    private static string Wrap(string text, int col)
    {
        const char separator = '\n';
        string result = string.Empty;
        
        if (text.Length <= col)
            return text;
        
        for (int index = 0; index < text.Length; index++)
        {
            if(index > 0 && index % col == 0)
                result += separator; 
            
            result += text[index];
        }
        
        return result;
    }
}