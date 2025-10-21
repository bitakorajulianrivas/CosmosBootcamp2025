using FluentAssertions;
using KatasTDD.Domain.BattleShips;
using KatasTDD.Domain.BattleShips.Enums;

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
    public void Player_ShouldNotHaveShipPlacedOnBoard()
    {
        var player = new Player(nickname: "CaptainAugustus");

        player.ShipsPlacedPerType
            .Select(shipPlaced => shipPlaced.Value)
            .Sum().Should().Be(0);
    }

    [Fact]
    public void Player_IfBegin_ShouldNotHaveCarriersPlacedOnBoard()
    {
        var player = new Player(nickname: "CaptainAugustus");

        player.ShipsPlacedPerType[ShipType.Carrier]
            .Should().Be(0);
    }

    [Fact]
    public void Player_IfBegin_ShouldNotHaveDestroyersPlacedOnBoard()
    {
        var player = new Player(nickname: "CaptainAugustus");

        player.ShipsPlacedPerType[ShipType.Destroyer]
            .Should().Be(0);
    }

    [Fact]
    public void Player_IfBegin_ShouldNotHaveGunshipsPlacedOnBoard()
    {
        var player = new Player(nickname: "CaptainAugustus");

        player.ShipsPlacedPerType[ShipType.Gunship]
            .Should().Be(0);
    }

    [Fact]
    public void PlaceShipOnBoard_IfThereAreNoShipsPlaced_ShouldAddShipsPlacedPerType()
    {
        var player = new Player(nickname: "CaptainAugustus");
        Ship ship = new Ship(ShipType.Gunship, coordinates: (X: 5, Y: 5));

        player.PlaceShipOnBoard(ship);

        player.ShipsPlacedPerType[ShipType.Gunship]
            .Should().Be(1);
    }

}