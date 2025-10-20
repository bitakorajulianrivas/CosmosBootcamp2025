using FluentAssertions;

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

        for (int column = 0; column < 10; column++)
        {
            for (int row = 0; row < 10; row++)
            {
                board.Cells[column, row].Should().Be(" "); 
            }
        }
    }
}

public class Player(string nickname)
{
    public string Nickname { get; } = nickname;
    public Board Board => new ();
}

public class Board
{
    public const int Columns = 10;
    public const int Rows = 10;


    public string[,] Cells { get; }

    public Board()
    {
        Cells = new string[Columns, Rows];

        for (int column = 0; column < 10; column++)
        {
            for (int row = 0; row < 10; row++)
            {
                Cells[column, row] = " ";
            }
        }
    }
}