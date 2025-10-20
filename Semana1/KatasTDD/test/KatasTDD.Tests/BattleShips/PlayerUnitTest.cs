using FluentAssertions;

namespace KatasTDD.Tests.BattleShips;

public class PlayerUnitTest
{
    [Fact]
    public void Player_ShouldHaveANickName()
    {
        var player = new Player(Nickname: "CaptainAugustus");
        player.Nickname.Should().Be("CaptainAugustus");
    }

    [Fact]
    public void Player_ShouldHaveABoard()
    {
        var player = new Player(Nickname: "CaptainAugustus");
        player.Board.Should().NotBeNull();
    }
}

public record Player(string Nickname)
{
    public Board Board => new();
}

public class Board { }