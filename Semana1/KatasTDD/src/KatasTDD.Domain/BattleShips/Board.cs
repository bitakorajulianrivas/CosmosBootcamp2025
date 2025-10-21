using KatasTDD.Domain.BattleShips.Enums;

namespace KatasTDD.Domain.BattleShips;

public class Board
{
    public static readonly string EmptyCell = " ";

    public const int Columns = 10;
    public const int Rows = 10;

    public string[,] Cells { get; }

    public Board()
    {
        Cells = new string[Columns, Rows];

        for (int column = 0; column < Columns; column++)
        for (int row = 0; row < Rows; row++)
            Cells[column, row] = EmptyCell;
    }

    public void PlaceShip(Ship ship)
    {
        foreach (var position in ship.GetPositions())
        {
            if(position.X >= Columns || position.Y >= Columns)
                throw new Exception("The ship's position is out of the board bounds.");


            if (ship.ShipType == ShipType.Carrier)
                Cells[position.X, position.Y] = "C";

            if (ship.ShipType == ShipType.Gunship)
                Cells[position.X, position.Y] = "G";
        }
    }
}