using KatasTDD.Domain.BattleShips.Enums;

namespace KatasTDD.Domain.BattleShips;

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