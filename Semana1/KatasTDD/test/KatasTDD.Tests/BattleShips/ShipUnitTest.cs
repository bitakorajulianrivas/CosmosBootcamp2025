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
        var ship = new Ship(size: 3);

        ship.Size.Should().Be(3);
    }

    [Fact]
    public void Ship_IfShipIsCarrier_ShouldHaveASize()
    {
        var carrier = new Ship(ShipType.Carrier, size: 1);
        carrier.Size.Should().Be(1);
    }
}

public class Ship
{
    public Ship(int size)
    {
        Size = size;
    }
    public Ship(ShipType Type, int size)
    {
        throw new NotImplementedException();
    }

    public object Size { get; }
}