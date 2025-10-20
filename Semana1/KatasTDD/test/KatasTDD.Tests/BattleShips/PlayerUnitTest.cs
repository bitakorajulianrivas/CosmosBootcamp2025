using FluentAssertions;
using Microsoft.VisualStudio.CodeCoverage;

namespace KatasTDD.Tests.BattleShips;

public class PlayerUnitTest
{
    [Fact]
    public void Player_ShouldHaveANickName()
    {
        var player = new Player(nickname: "CaptainAugustus");

        player.Nickname.Should().Be("CaptainAugustus");
    }

    [Fact]
    public void Player_ShouldHaveABoard()
    {
        var player = new Player(nickname: "CaptainAugustus");

        player.Board.Should().NotBeNull();
    }

    [Fact]
    public void Player_ShouldHave3TypeOfShipsToPlace()
    {
        var player = new Player(nickname: "CaptainAugustus");

        player.ShipType.Should().HaveCount(3);
    }

    [Fact]
    public void Player_ShouldHave3TypeOfShipsToPlace_Carriers_Destroyers_AndGunShips()
    {
        var player = new Player(nickname: "CaptainAugustus");

        player.ShipType[0].Name.Should().Be("Carrier");
        player.ShipType[1].Name.Should().Be("Destroyer");
        player.ShipType[2].Name.Should().Be("Gunship");
    }


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

public class Player(string nickname)
{
    public string Nickname { get; } = nickname;
    public Board Board => new ();
    public ShipType[] ShipType { get; } = [
        new ShipType("Carrier"), 
        new ShipType("Destroyer"), 
        new ShipType("Gunship")];
}

public class ShipType
{
    public ShipType(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
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