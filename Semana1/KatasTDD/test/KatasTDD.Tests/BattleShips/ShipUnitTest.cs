using FluentAssertions;

namespace KatasTDD.Tests.BattleShips;

public class ShipUnitTest
{
    [Fact]
    public void Carrier_ShouldHaveSizeOfFourCells_AndMaxAmountOfOneShip()
    {
        var carrier = Ship.Carrier();

        carrier.Size.Should().Be(4);
        carrier.MaxAmount.Should().Be(1);
    }

    [Fact]
    public void Destoyer_ShouldHaveSizeOfThreeCells()
    {
        var carrier = Ship.Destroyer();

        carrier.Size.Should().Be(3);
    }

    [Fact]
    public void Gunship_ShouldHaveSizeOfOne()
    {
        var carrier = Ship.Gunship();

        carrier.Size.Should().Be(1);
    }
}

public class Ship
{
    public ShipType ShipType { get; }
    public int Size { get; }
    public int MaxAmount { get; }

    private Ship(ShipType shipType, int size, int maxAmount)
    {
        ShipType = shipType;
        Size = size;
        MaxAmount = maxAmount;
    }

    public static Ship Carrier() => new (ShipType.Carrier, size: 4, maxAmount: 1);
    public static Ship Destroyer() => new (ShipType.Destroyer, size: 3, maxAmount: 1);
    public static Ship Gunship() => new (ShipType.Gunship, size: 1, maxAmount: 1);
}

public enum ShipType
{
    Carrier = 0,
    Destroyer = 1,
    Gunship = 2
};