using FluentAssertions;

namespace KatasTDD.Tests.BattleShips;

public class BoardUnitTest
{
    [Fact]
    public void Board_ShouldHave10Columns()
    {
        Board.Columns.Should().Be(10);
    }

    [Fact]
    public void Board_ShouldHave10Rows()
    {
        Board.Rows.Should().Be(10);
    }

    [Fact]
    public void Board_ShouldHave10x10Cells()
    {
        var board = new Board();

        board.Cells.GetLength(0).Should().Be(10);
        board.Cells.GetLength(1).Should().Be(10);
    }

    [Fact]
    public void Board_ShouldStartEmpty()
    {
        var board = new Board();

        board.AssertAllCellsAreEmpty();
    }
}

public class Board
{
    public static readonly string EmptyCell = " ";

    public const int Columns = 10;
    public const int Rows = 10;

    public string[,] Cells { get; }

    public Board()
    {
        Cells = new string[Columns, Rows];

        for (int column = 0; column < Columns; column++)
        for (int row = 0; row < Rows; row++)
            Cells[column, row] = EmptyCell;
    }
}

public static class BoardTestExtensions
{
    public static void AssertAllCellsAreEmpty(this Board board)
    {
        for (int column = 0; column < Board.Columns; column++)
        for (int row = 0; row < Board.Columns; row++)
            board.Cells[column, row].Should().Be(Board.EmptyCell);
    }
}