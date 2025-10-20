using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;

namespace KatasTDD.Tests.BattleShips;

public class ShipUnitTest
{
    [Fact]
    public void Ship_ShouldHaveASize()
    {
        var ship = new Ship(ShipType.Destroyer, size: 3);

        ship.Size.Should().Be(3);
    }
}

public class Ship
{
    public Ship(ShipType type, int size)
    {
        throw new NotImplementedException();
    }

    public object Size { get; set; }
}