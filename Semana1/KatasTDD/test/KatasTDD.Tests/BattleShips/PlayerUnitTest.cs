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
        var board = new Board();

        board.Columns.Should().Be(10);
    }
}

public class Player(string nickname)
{
    public string Nickname { get; } = nickname;
    public Board Board => new();
}

public record Board
{
    public object Columns { get; } = 10;
}