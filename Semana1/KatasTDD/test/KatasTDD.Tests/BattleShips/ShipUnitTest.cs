using FluentAssertions;
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

        ship.Position.Should().Be((ShipPosition) 1);
    }

}

public class Ship
{
    public Ship(ShipType shipType, (int X, int Y) coordinates, ShipPosition position = ShipPosition.Horizontal)
    {
        ShipType = shipType;
        Coordinates = coordinates;
        Position = position;
    }

    public ShipType ShipType { get; }
    public (int X, int Y) Coordinates { get; }
    public ShipPosition Position { get; set; }
}

public enum ShipPosition : byte
{
    Horizontal = 0,
    Vertical = byte.MaxValue
}