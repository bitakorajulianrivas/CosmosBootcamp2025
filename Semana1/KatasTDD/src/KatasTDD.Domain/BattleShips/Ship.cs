using KatasTDD.Domain.BattleShips.Enums;

namespace KatasTDD.Domain.BattleShips;

public class Ship
{
    public ShipType ShipType { get; }
    public (int X, int Y) Coordinates { get; }
    public ShipDirection Direction { get; }

    public Ship(ShipType shipType,
        (int X, int Y) coordinates, 
        ShipDirection direction = ShipDirection.Horizontal)
    {
        ValidateCoordinates(coordinates);

        ShipType = shipType;
        Coordinates = coordinates;
        Direction = direction;
    }

    private void ValidateCoordinates((int X, int Y) coordinates)
    {
        if (coordinates.X < 0 || 
            coordinates.Y < 0 ||
            coordinates.X >= Board.Columns || 
            coordinates.Y >= Board.Columns)
            throw new Exception("The ship's direction is outside the valid board interval.");
    }

    public int GetSize() => ShipSpecification
        .ShipsSpecificationList[ShipType].Size;

    public char GetLetter() => ShipSpecification
        .ShipsSpecificationList[ShipType].Letter;

    public int GetMaxShipsPerType() => ShipSpecification
        .ShipsSpecificationList[ShipType].MaxAmount;

    public (int X, int Y)[] GetPositions()
    {
        if (Direction == ShipDirection.Vertical)
            return GetVerticalPositions();

        return GetHorizontalPositions();
    }

    private (int X, int Y)[] GetVerticalPositions()
    {
        var positions = new (int X, int Y)[GetSize()];
        
        for (int rows = 0; rows < GetSize(); rows++) 
            positions[rows] = (Coordinates.X, Coordinates.Y + rows);

        return positions;
    }

    private (int X, int Y)[] GetHorizontalPositions()
    {
        var positions = new (int X, int Y)[GetSize()];

        for (int columns = 0; columns < GetSize(); columns++) 
            positions[columns] = (Coordinates.X + columns, Coordinates.Y);

        return positions;
    }
}