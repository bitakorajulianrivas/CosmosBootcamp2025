using FluentAssertions;
using KatasTDD.Domain.BattleShips.Enums;

namespace KatasTDD.Tests.BattleShips;

public class ShipSpecificationUnitTest
{
    [Fact]
    public void Carrier_ShouldHaveSizeOfFourCells_AndMaxAmountOfOneShip()
    {
        var carrier = Domain.BattleShips.ShipSpecification.Carrier();

        carrier.ShipType.Should().Be(ShipType.Carrier);
        carrier.Size.Should().Be(4);
        carrier.MaxAmount.Should().Be(1);
    }

    [Fact]
    public void Destroyer_ShouldHaveSizeOfThreeCells_AndMaxAmountOfTwoShips()
    {
        var destroyer = Domain.BattleShips.ShipSpecification.Destroyer();

        destroyer.ShipType.Should().Be(ShipType.Destroyer);
        destroyer.Size.Should().Be(3);
        destroyer.MaxAmount.Should().Be(2);
    }

    [Fact]
    public void Gunship_ShouldHaveSizeOfOne_AndMaxAmountOfFourShips()
    {
        var gunship = Domain.BattleShips.ShipSpecification.Gunship();

        gunship.ShipType.Should().Be(ShipType.Gunship);
        gunship.Size.Should().Be(1);
        gunship.MaxAmount.Should().Be(4);
    }

    [Fact]
    public void Carrier_ShouldHaveLetter_C()
    {
        var carrier = Domain.BattleShips.ShipSpecification.Carrier();

        carrier.Letter.Should().Be('C');
    }

    [Fact]
    public void Destroyer_ShouldHaveLetter_D()
    {
        var destroyer = Domain.BattleShips.ShipSpecification.Destroyer();

        destroyer.Letter.Should().Be('D');
    }

    [Fact]
    public void Gunship_ShouldHaveLetter_G()
    {
        var gunship = Domain.BattleShips.ShipSpecification.Gunship();

        gunship.Letter.Should().Be('G');
    }
}