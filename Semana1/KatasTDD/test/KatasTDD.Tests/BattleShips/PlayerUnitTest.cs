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
    public void PlaceShipOnBoard_ShouldPlaceShipInsideBoard()
    {
        var player = new Player(nickname: "CaptainAugustus");
        Ship ship = new Ship(ShipType.Gunship, coordinates: (X: 5, Y: 5));

        player.PlaceShipOnBoard(ship);

        player.Board.Cells[5,5]
            .Should().Be('G');
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

    [Fact]
    public void PlaceShipOnBoard_IfAlreadyPlacedACarrierOnBoard_ShouldThrowException()
    {
        var player = new Player(nickname: "CaptainAugustus");

        var firstCarrier = new Ship(ShipType.Carrier, coordinates: (0, 0));
        player.PlaceShipOnBoard(firstCarrier);

        Ship secondCarrier = new Ship(ShipType.Carrier, coordinates: (X: 5, Y: 5));
        Action action = () => player.PlaceShipOnBoard(secondCarrier);

        action.Should().ThrowExactly<Exception>()
            .WithMessage("All carriers have been placed on the board.");
    }

    [Fact]
    public void ()
    {
        var player = new Player(nickname: "CaptainAugustus");

        var firstDestroyer = new Ship(ShipType.Destroyer, coordinates: (3, 3));
        player.PlaceShipOnBoard(firstDestroyer);

        var secondDestroyer = new Ship(ShipType.Destroyer, coordinates: (4, 4));
        player.PlaceShipOnBoard(secondDestroyer);

        Ship thirdDestroyer = new Ship(ShipType.Destroyer, coordinates: (X: 5, Y: 5));

        Action action = () => player.PlaceShipOnBoard(thirdDestroyer);

        action.Should().ThrowExactly<Exception>()
            .WithMessage("All destroyers have been placed on the board.");
    }
}