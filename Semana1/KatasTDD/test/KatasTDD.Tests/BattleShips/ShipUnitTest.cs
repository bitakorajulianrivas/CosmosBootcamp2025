using FluentAssertions;
using KatasTDD.Domain.BattleShips;
using KatasTDD.Domain.BattleShips.Enums;

namespace KatasTDD.Tests.BattleShips;

public class ShipUnitTest
{
    [Fact]
    public void Ship_ShouldHaveCoordinates()
    {
        var ship = new Ship(ShipType.Carrier, coordinates: (X: 0, Y: 0));

        ship.Coordinates.Should().Be((0, 0));
    }

    [Fact]
    public void Ship_IfColsNumberExceedTheMaximumColsAllowed_ShouldThrowException()
    {
        int colsNumber = 10;

        Action action = () => new Ship(ShipType.Carrier, 
            coordinates: (X: colsNumber, Y: 0));

        action.Should().ThrowExactly<Exception>()
            .WithMessage("The ship's position is outside the valid board interval.");
    }


    [Fact]
    public void Ship_ShouldHaveHorizontalPositionAsDefault()
    {
        var ship = new Ship(ShipType.Carrier, coordinates: (X: 0, Y: 0));

        ship.Position.Should().Be(ShipPosition.Horizontal);
    }

    [Fact]
    public void Ship_ShouldHaveVerticalPosition()
    {
        ShipPosition shipPosition = ShipPosition.Vertical;

        var ship = new Ship(ShipType.Carrier, 
            coordinates: (X: 0, Y: 0), 
            shipPosition);

        ship.Position.Should().Be(ShipPosition.Vertical);
    }

    [Fact]
    public void GetSize_IfShipTypeIsCarrier_ShouldReturnCarrierSize()
    {
        var ship = new Ship(ShipType.Carrier, 
            coordinates: (X: 0, Y: 0));

        int size = ship.GetSize();

        size.Should().Be(4);
    }

    [Fact]
    public void GetSize_IfShipTypeIsDestroyer_ShouldReturnDestroyerSize()
    {
        var ship = new Ship(ShipType.Destroyer,
            coordinates: (X: 0, Y: 0));

        int size = ship.GetSize();

        size.Should().Be(3);
    }

    [Fact]
    public void GetSize_IfShipTypeIsGunShip_ShouldReturnGunShipSize()
    {
        var ship = new Ship(ShipType.Gunship,
            coordinates: (X: 0, Y: 0));

        int size = ship.GetSize();

        size.Should().Be(1);
    }    
}

public class Ship
{
    public (int X, int Y) Coordinates { get; }
    public ShipPosition Position { get; }
    private ShipType ShipType { get; }

    public Ship(ShipType shipType,
        (int X, int Y) coordinates, 
        ShipPosition position = ShipPosition.Horizontal)
    {
        ValidateCoordinates(coordinates);

        ShipType = shipType;
        Coordinates = coordinates;
        Position = position;
    }

    private void ValidateCoordinates((int X, int Y) coordinates)
    {
        if (coordinates.X >= Board.Columns)
            throw new Exception("The ship's position is outside the valid board interval.");
    }

    public int GetSize() => ShipSpecification
        .ShipsSpecificationList[ShipType].Size;
}

public enum ShipPosition : byte
{
    Horizontal = 0,
    Vertical = 1
}