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
}