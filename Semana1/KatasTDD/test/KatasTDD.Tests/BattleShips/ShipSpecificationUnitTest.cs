using FluentAssertions;

namespace KatasTDD.Tests.BattleShips;

public class ShipSpecificationUnitTest
{
    [Fact]
    public void Carrier_ShouldHaveSizeOfFourCells_AndMaxAmountOfOneShip()
    {
        var carrier = ShipSpecification.Carrier();

        carrier.ShipType.Should().Be(ShipType.Carrier);
        carrier.Size.Should().Be(4);
        carrier.MaxAmount.Should().Be(1);
    }

    [Fact]
    public void Destroyer_ShouldHaveSizeOfThreeCells_AndMaxAmountOfTwoShips()
    {
        var destroyer = ShipSpecification.Destroyer();

        destroyer.ShipType.Should().Be(ShipType.Destroyer);
        destroyer.Size.Should().Be(3);
        destroyer.MaxAmount.Should().Be(2);
    }

    [Fact]
    public void Gunship_ShouldHaveSizeOfOne_AndMaxAmountOfFourShips()
    {
        var gunship = ShipSpecification.Gunship();

        gunship.ShipType.Should().Be(ShipType.Gunship);
        gunship.Size.Should().Be(1);
        gunship.MaxAmount.Should().Be(4);
    }

    [Fact]
    public void Carrier_ShouldHaveIcon_C()
    {
        var carrier = ShipSpecification.Carrier();

        carrier.Icon.Should().Be('C');
    }
}

public class ShipSpecification
{
    public ShipType ShipType { get; }
    public int Size { get; }
    public int MaxAmount { get; }
    public object Icon { get; set; }

    private ShipSpecification(ShipType shipType, int size, int maxAmount, object icon)
    {
        ShipType = shipType;
        Size = size;
        MaxAmount = maxAmount;
        Icon = icon;
    }

    public static ShipSpecification Carrier() => new (ShipType.Carrier, 
        size: 4, maxAmount: 1, icon: 'C');

    public static ShipSpecification Destroyer() => new (ShipType.Destroyer, 
        size: 3, maxAmount: 2, icon: string.Empty);

    public static ShipSpecification Gunship() => new (ShipType.Gunship, 
        size: 1, maxAmount: 4, icon: string.Empty);
}

public enum ShipType
{
    Carrier = 0,
    Destroyer = 1,
    Gunship = 2
};