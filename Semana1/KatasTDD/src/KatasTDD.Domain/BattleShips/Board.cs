using KatasTDD.Domain.BattleShips.Enums;

namespace KatasTDD.Domain.BattleShips;

public class Board
{
    public static readonly char EmptyCell = ' ';

    public const int Columns = 10;
    public const int Rows = 10;

    public char[,] Cells { get; }

    public Board()
    {
        Cells = new char[Columns, Rows];

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

                Cells[position.X, position.Y] = ship.GetLetter();
        }
    }
}