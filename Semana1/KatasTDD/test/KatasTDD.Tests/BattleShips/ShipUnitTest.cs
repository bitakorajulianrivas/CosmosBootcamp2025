using FluentAssertions;
using KatasTDD.Domain.BattleShips.Enums;

namespace KatasTDD.Tests.BattleShips;

public class ShipUnitTest
{
    [Fact]
    public void Ship_ShouldHaveCoordinates()
    {
        var ship = new Ship(ShipType.Carrier, coordinates: (X: 0, Y: 0), position: 0);

        ship.Coordinates.Should().Be((0, 0));
    }

    [Fact]
    public void Ship_ShouldHavePosition()
    {
        var ship = new Ship(ShipType.Carrier, coordinates: (X: 0, Y: 0), position: 0);

        ship.Position.Should().Be(0);
    }
}

public class Ship
{
    public Ship(ShipType shipType, (int X, int Y) coordinates, int position)
    {
        ShipType = shipType;
        Coordinates = coordinates;
    }

    public ShipType ShipType { get; }
    public (int X, int Y) Coordinates { get; }
    public object? Position { get; set; }
}