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
    public void Carrier_ShouldHaveLetter_C()
    {
        var carrier = ShipSpecification.Carrier();

        carrier.Letter.Should().Be('C');
    }

    [Fact]
    public void Destroyer_ShouldHaveLetter_D()
    {
        var destroyer = ShipSpecification.Destroyer();

        destroyer.Letter.Should().Be('D');
    }

    [Fact]
    public void Gunship_ShouldHaveLetter_G()
    {
        var gunship = ShipSpecification.Gunship();

        gunship.Letter.Should().Be('G');
    }
}

public class ShipSpecification
{
    public ShipType ShipType { get; }
    public int Size { get; }
    public int MaxAmount { get; }
    public char Letter { get; set; }

    private ShipSpecification(ShipType shipType, int size, int maxAmount, char letter)
    {
        ShipType = shipType;
        Size = size;
        MaxAmount = maxAmount;
        Letter = letter;
    }

    public static ShipSpecification Carrier() => new (ShipType.Carrier, 
        size: 4, maxAmount: 1, letter: 'C');

    public static ShipSpecification Destroyer() => new (ShipType.Destroyer, 
        size: 3, maxAmount: 2, letter: 'D');

    public static ShipSpecification Gunship() => new (ShipType.Gunship, 
        size: 1, maxAmount: 4, letter: 'G');
}

public enum ShipType
{
    Carrier = 0,
    Destroyer = 1,
    Gunship = 2
};