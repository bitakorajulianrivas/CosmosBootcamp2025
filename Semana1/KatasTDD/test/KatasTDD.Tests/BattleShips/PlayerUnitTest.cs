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
    public void Player_ShouldHave3TypeOfShipsToPlace()
    {
        var player = new Player(nickname: "CaptainAugustus");

        player.ShipTypes.Should().HaveCount(3);
    }

    [Fact]
    public void Player_ShouldHave3TypeOfShipsToPlace_Carriers_Destroyers_AndGunShips()
    {
        var player = new Player(nickname: "CaptainAugustus");

        player.ShipTypes.Should().ContainKey(ShipType.Carrier);
        player.ShipTypes.Should().ContainKey(ShipType.Destroyer);
        player.ShipTypes.Should().ContainKey(ShipType.Gunship);
    }

    [Fact]
    public void Player_ShouldHave3TypeOfShipsToPlace_1Carriers2Destroyers_And4GunShips()
    {
        var player = new Player(nickname: "CaptainAugustus");

        player.ShipTypes[ShipType.Carrier].Should().Be(1);
        player.ShipTypes[ShipType.Destroyer].Should().Be(2);
        player.ShipTypes[ShipType.Gunship].Should().Be(4);
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

    public Dictionary<ShipType, int> ShipTypes { get; } =        new () {
            {ShipType.Carrier, 1},
            {ShipType.Destroyer, 2},
            {ShipType.Gunship, 4} };
}

public enum ShipType
{
    Carrier = 0,
    Destroyer = 1,
    Gunship = 2
};

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