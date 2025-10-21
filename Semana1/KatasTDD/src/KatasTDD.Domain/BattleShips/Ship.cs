using KatasTDD.Domain.BattleShips.Enums;

namespace KatasTDD.Domain.BattleShips;

public class Ship
{
    public (int X, int Y) Coordinates { get; }
    public ShipDirection Direction { get; }
    private ShipType ShipType { get; }

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
        if (coordinates.X == -1 ||
            coordinates.X >= Board.Columns || 
            coordinates.Y >= Board.Columns)
            throw new Exception("The ship's direction is outside the valid board interval.");
    }

    public int GetSize() => ShipSpecification
        .ShipsSpecificationList[ShipType].Size;

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