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
    public void Player_IfBegin_ShouldNotHaveShipPlacedOnBoard()
    {
        var player = new Player(nickname: "CaptainAugustus");

        player.ShipsPlaced.Should().HaveCount(0);
    }

    [Fact]
    public void Player_IfBegin_ShouldNotHaveCarriersPlacedOnBoard()
    {
        var player = new Player(nickname: "CaptainAugustus");


        player.ShipsPlaced[ShipType.Carrier].Should().Be(0);
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

    public Dictionary<ShipType, int> ShipsPlaced => new ()
    {
    };
}

public enum ShipType
{
    Carrier = 0,
    Destroyer = 1,
    Gunship = 2
};