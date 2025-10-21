using FluentAssertions;
using KatasTDD.Domain.BattleShips.Enums;

namespace KatasTDD.Tests.BattleShips;

public class ShipUnitTest
{
    [Fact]
    public void Ship_ShouldHaveCoordinates()
    {
        var ship = new Ship(ShipType.Carrier, (0, 0));

        ship.Coordinates.Should().Be((x: 0, y: 0));
    }
}

public class Ship
{
    public Ship(ShipType shipType, (int, int) coordinates)
    {
        throw new NotImplementedException();
    }

    public object Coordinates { get; }
}