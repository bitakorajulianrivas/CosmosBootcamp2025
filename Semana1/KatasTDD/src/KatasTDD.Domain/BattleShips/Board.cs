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

        for (int row = 0; row < Columns; row++)
            for (int column = 0; column < Rows; column++)
                Cells[column, row] = EmptyCell;
    }

    public void PlaceShip(Ship ship)
    {
        foreach (var position in ship.GetPositions())
        {
            if(position.X >= Columns || position.Y >= Columns)
                throw new Exception("The ship's position is out of the board bounds.");

            if (Cells[position.X, position.Y] != ' ')
                throw new Exception($"There is a ship in the position {position.ToString()}.");

            Cells[position.X, position.Y] = ship.GetLetter();
        }
    }

    public string Print()
    {
        string board = "\n" +
           "   | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | \n" +
           "-------------------------------------------| \n";

        for (int row = 0; row < Rows; row++)
        {
            board += $" {row} |";
            for (int column = 0; column <  Columns; column++)
            {
                board += $" {Cells[column, row]} |";
            }

            board += " \n";
        }

        board += 
            "-------------------------------------------| \n\n";

        return board;
    }
}