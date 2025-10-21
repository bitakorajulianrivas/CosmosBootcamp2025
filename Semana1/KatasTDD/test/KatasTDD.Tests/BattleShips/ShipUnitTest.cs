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
            .WithMessage("The ship's direction is outside the valid board interval.");
    }

    [Fact]
    public void Ship_IfRowsNumberExceedTheMaximumRowsAllowed_ShouldThrowException()
    {
        int rowsNumber = 10;

        Action action = () => new Ship(ShipType.Carrier,
            coordinates: (X: 0, Y: rowsNumber));

        action.Should().ThrowExactly<Exception>()
            .WithMessage("The ship's direction is outside the valid board interval.");
    }


    [Fact]
    public void Ship_ShouldHaveHorizontalDirectionAsDefault()
    {
        var ship = new Ship(ShipType.Carrier, coordinates: (X: 0, Y: 0));

        ship.Direction.Should().Be(ShipDirection.Horizontal);
    }

    [Fact]
    public void Ship_ShouldHaveVerticalDirection()
    {
        ShipDirection shipDirection = ShipDirection.Vertical;

        var ship = new Ship(ShipType.Carrier, 
            coordinates: (X: 0, Y: 0), 
            shipDirection);

        ship.Direction.Should().Be(ShipDirection.Vertical);
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

    [Fact]
    public void GetPositions_IfShipTypeIsGunShip_ShouldReturnOneCoordinate()
    {
        var ship = new Ship(ShipType.Gunship,
            coordinates: (X: 0, Y: 0));

        var positions = ship.GetPositions();

        positions.Should().HaveCount(1);
        positions.Should().BeEquivalentTo([(0,0)]);
    }
}

public class Ship
{
    public (int X, int Y) Coordinates { get; }
    public ShipDirection Direction { get; }
    private ShipType ShipType { get; }

    public Ship(ShipType shipType,
        (int X, int Y) coordinates, 
        ShipDirection direction = ShipDirection.Horizontal)
    {
        ValidateCoordinates(coordinates);

        ShipType = shipType;
        Coordinates = coordinates;
        Direction = direction;
    }

    private void ValidateCoordinates((int X, int Y) coordinates)
    {
        if (coordinates.X >= Board.Columns || 
            coordinates.Y >= Board.Columns)
            throw new Exception("The ship's direction is outside the valid board interval.");
    }

    public int GetSize() => ShipSpecification
        .ShipsSpecificationList[ShipType].Size;

    public (int X, int Y)[] GetPositions()
    {
        return [Coordinates];
    }
}

public enum ShipDirection : byte
{
    Horizontal = 0,
    Vertical = 1
}