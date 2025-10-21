using FluentAssertions;
using KatasTDD.Domain.BattleShips.Enums;
using Microsoft.VisualStudio.CodeCoverage;

namespace KatasTDD.Tests.BattleShips;

public class ShipUnitTest
{
    [Fact]
    public void Ship_ShouldHaveCoordinates()
    {
        var ship = new Ship(ShipType.Carrier, Coordinates: (X: 0, Y: 0));

        ship.Coordinates.Should().Be((0, 0));
    }

    [Fact]
    public void Ship_ShouldHaveHorizontalPositionAsDefault()
    {
        var ship = new Ship(ShipType.Carrier, Coordinates: (X: 0, Y: 0));

        ship.Position.Should().Be(ShipPosition.Horizontal);
    }

    [Fact]
    public void Ship_ShouldHaveVerticalPosition()
    {
        ShipPosition shipPosition = ShipPosition.Vertical;

        var ship = new Ship(ShipType.Carrier, 
            Coordinates: (X: 0, Y: 0), 
            shipPosition);

        ship.Position.Should().Be(ShipPosition.Vertical);
    }

    [Fact]
    public void GetSize_IfShipTypeIsCarrier_ShouldReturnCarrierSize()
    {
        var ship = new Ship(ShipType.Carrier, Coordinates: (X: 0, Y: 0));

        int size = ship.GetSize();

        size.Should().Be(4);
    }
}

public record Ship(ShipType ShipType,
    (int X, int Y) Coordinates, 
    ShipPosition Position = ShipPosition.Horizontal)
{
    public int GetSize()
    {
        throw new NotImplementedException();
    }
}

public enum ShipPosition : byte
{
    Horizontal = 0,
    Vertical = 1
}