using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xunit.Sdk;

namespace KatasTDD.Tests.BattleShips;

public class ShipUnitTest
{
    [Fact]
    public void Ship_ShouldHaveASize()
    {
        var ship = new Ship(ShipType.Destroyer);

        ship.Size.Should().Be(3);
    }

    [Fact]
    public void Ship_IfShipIsCarrier_ShouldHaveSizeOfFour()
    {
        var carrier = new Ship(ShipType.Carrier);

        carrier.Size.Should().Be(4);
    }

    [Fact]
    public void Ship_IfShipIsDestoyer_ShouldHaveSizeOfThree()
    {
        var carrier = new Ship(ShipType.Destroyer);

        carrier.Size.Should().Be(3);
    }
}

public class Ship
{
    public Ship(ShipType type)
    {
        Size = type == ShipType.Destroyer ? 3: 4;
    }

    public object Size { get; }
}