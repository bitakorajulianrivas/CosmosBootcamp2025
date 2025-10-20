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
}

public record Player(string Nickname);