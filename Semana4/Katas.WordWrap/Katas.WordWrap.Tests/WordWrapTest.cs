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
    
    [Fact]
    public void Wrap_IfInputTextHasTwoWordAndColumnNumerIsThree_ShouldInsertNewLineForEmptySpaceAndNewLineForEachThreeChars()
    {
        var result = Wrap("word word", 3);

        result.Should().Be("wor\nd\nwor\nd");
    }

    [Fact]
    public void Wrap_IfInputTextHasTwoWordAndColumnNumerIsSix_ShouldInsertNewLineForEmptySpace()
    {
        var result = Wrap("word word", 6);

        result.Should().Be("word\nword");
    } 
    
    private static string Wrap(string text, int col)
    {
        const char separator = '\n';
        string[] wordsArray = text.Split(' ');
        List<string> result = new List<string>();
        
        foreach (var word in wordsArray) 
            result.Add(WrapByWord(word, col, separator));

        return string.Join(separator, result);
    }

    private static string WrapByWord(string text, int col, char separator)
    {
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