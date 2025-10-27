using FluentAssertions;
using KatasTDD.Domain.BattleShips;
using KatasTDD.Domain.BattleShips.Enums;

namespace KatasTDD.Tests.BattleShips;

public class BoardUnitTest
{
    [Fact]
    public void Board_ShouldHave10Columns()
    {
        Board.Columns.Should().Be(10);
    }

    [Fact]
    public void Board_ShouldHave10Rows()
    {
        Board.Rows.Should().Be(10);
    }

    [Fact]
    public void Board_ShouldHave10x10Cells()
    {
        var board = new Board();

        board.Cells.GetLength(0).Should().Be(10);
        board.Cells.GetLength(1).Should().Be(10);
    }

    [Fact]
    public void Board_ShouldStartEmpty()
    {
        var board = new Board();

        board.AssertAllCellsAreEmpty();
    }

    [Fact]
    public void PlaceShip_IfShipPositionsIsOutOfBounds_OnTheXAxis_ShouldThrowException()
    {
        var board = new Board();
        Ship ship = new Ship(ShipType.Carrier, coordinates: (X: 8, 5));

        Action action = () => board.PlaceShip(ship);

        action.Should().ThrowExactly<Exception>()
            .WithMessage("The ship's position is out of the board bounds.");
    }

    [Fact]
    public void PlaceShip_IfShipPositionsIsOutOfBounds_OnTheYAxis_ShouldThrowException()
    {
        var board = new Board();
        Ship ship = new Ship(ShipType.Carrier, coordinates: (X: 5, Y: 8), ShipDirection.Vertical);

        Action action = () => board.PlaceShip(ship);

        action.Should().ThrowExactly<Exception>()
            .WithMessage("The ship's position is out of the board bounds.");
    }

    [Fact]
    public void PlaceShip_IfPlaceGunshipAndThereIsNoShipInTheCoordinate_ShouldReplaceCells()
    {
        var board = new Board();
        Ship ship = new Ship(ShipType.Gunship, coordinates: (X: 5, Y: 5));

        board.PlaceShip(ship);

        board.Cells[5, 5].Should().Be('G');
    }

    [Fact]
    public void PlaceShip_IfPlaceCarrierAndThereAreNoShipsInTheCoordinates_ShouldReplaceCells()
    {
        var board = new Board();
        Ship ship = new Ship(ShipType.Carrier, coordinates: (X: 3, Y: 0));

        board.PlaceShip(ship);

        board.Cells[3, 0].Should().Be('C');
        board.Cells[4, 0].Should().Be('C');
        board.Cells[5, 0].Should().Be('C');
        board.Cells[6, 0].Should().Be('C');
    }

    [Fact]
    public void PlaceShip_IfPlaceDestroyerAndThereAreNoShipsInTheCoordinates_ShouldReplaceCells()
    {
        var board = new Board();
        Ship ship = new Ship(ShipType.Destroyer, coordinates: (X: 5, Y: 5),
            ShipDirection.Vertical);

        board.PlaceShip(ship);

        board.Cells[5, 5].Should().Be('D');
        board.Cells[5, 6].Should().Be('D');
        board.Cells[5, 7].Should().Be('D');
    }

    [Fact]
    public void PlaceShip_IfThereIsAShipInTheCoordinate_ShouldThrowException()
    {
        var board = new Board();
        Ship gunship = new Ship(ShipType.Gunship, coordinates: (X: 5, Y: 5));
        board.PlaceShip(gunship);
        Ship destroyer = new Ship(ShipType.Destroyer, coordinates: (X: 5, Y: 5));

        Action action = () => board.PlaceShip(destroyer);

        action.Should().ThrowExactly<Exception>()
            .WithMessage("There is a ship in the position (5, 5).");
    }

    [Fact]
    public void Print_IfThereAreNoShipsPlaced_ShoudReturnEmptyBoard()
    {
        var expectedBoard = "\n" +
            "   | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | \n" +
            "-------------------------------------------| \n" +
            " 0 |   |   |   |   |   |   |   |   |   |   | \n" +
            " 1 |   |   |   |   |   |   |   |   |   |   | \n" +
            " 2 |   |   |   |   |   |   |   |   |   |   | \n" +
            " 3 |   |   |   |   |   |   |   |   |   |   | \n" +
            " 4 |   |   |   |   |   |   |   |   |   |   | \n" +
            " 5 |   |   |   |   |   |   |   |   |   |   | \n" +
            " 6 |   |   |   |   |   |   |   |   |   |   | \n" +
            " 7 |   |   |   |   |   |   |   |   |   |   | \n" +
            " 8 |   |   |   |   |   |   |   |   |   |   | \n" +
            " 9 |   |   |   |   |   |   |   |   |   |   | \n" +
            "-------------------------------------------| \n" +
            "\n";

        var board = new Board();


        string boardPrinted = board.Print();


        boardPrinted.Should().Be(expectedBoard);
    }

    [Fact]
    public void Print_IfThereIsACarrierPlaced_ShouldReturnBoardWithShip()
    {
        var expectedBoard = "\n" +
            "   | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | \n" +
            "-------------------------------------------| \n" +
            " 0 |   |   |   |   |   |   |   |   |   |   | \n" +
            " 1 |   |   |   |   |   |   |   |   |   |   | \n" +
            " 2 |   |   |   |   |   |   |   |   |   |   | \n" +
            " 3 |   |   |   |   |   |   |   |   |   |   | \n" +
            " 4 |   |   | C | C | C | C |   |   |   |   | \n" +
            " 5 |   |   |   |   |   |   |   |   |   |   | \n" +
            " 6 |   |   |   |   |   |   |   |   |   |   | \n" +
            " 7 |   |   |   |   |   |   |   |   |   |   | \n" +
            " 8 |   |   |   |   |   |   |   |   |   |   | \n" +
            " 9 |   |   |   |   |   |   |   |   |   |   | \n" +
            "-------------------------------------------| \n" +
            "\n";

        var board = new Board();

        Ship carrier = new Ship(ShipType.Carrier, 
            coordinates: (2, 4));

        board.PlaceShip(carrier);

        string boardPrinted = board.Print();


        boardPrinted.Should().Be(expectedBoard);
    }

    [Fact]
    public void Print_IfAllShipsArePlaced_ShouldReturnBoardWithShips()
    {
        var expectedBoard = "\n" +
            "   | 0 | 1 | 2 | 3 | 4 | 5 | 6 | 7 | 8 | 9 | \n" +
            "-------------------------------------------| \n" +
            " 0 |   |   |   |   |   |   |   |   |   |   | \n" +
            " 1 |   | G |   |   |   |   |   |   |   |   | \n" +
            " 2 |   |   |   |   |   | G |   |   | D |   | \n" +
            " 3 |   |   |   |   |   |   |   |   | D |   | \n" +
            " 4 |   |   | C | C | C | C |   |   | D |   | \n" +
            " 5 |   |   |   |   |   |   |   |   |   |   | \n" +
            " 6 |   |   |   |   |   |   |   |   |   |   | \n" +
            " 7 |   | G |   |   | D | D | D |   |   |   | \n" +
            " 8 |   |   |   |   |   |   |   |   |   | G | \n" +
            " 9 |   |   |   |   |   |   |   |   |   |   | \n" +
            "-------------------------------------------| \n" +
            "\n";

        var board = new Board();

        Ship carrier = new Ship(ShipType.Carrier, coordinates: (2, 4));
        Ship destroyer1 = new Ship(ShipType.Destroyer, coordinates: (8, 2), ShipDirection.Vertical);
        Ship destroyer2 = new Ship(ShipType.Destroyer, coordinates: (4, 7));
        Ship gunship1 = new Ship(ShipType.Gunship, coordinates: (1, 1));
        Ship gunship2 = new Ship(ShipType.Gunship, coordinates: (5, 2));
        Ship gunship3 = new Ship(ShipType.Gunship, coordinates: (1, 7));
        Ship gunship4 = new Ship(ShipType.Gunship, coordinates: (9, 8));
        
        board.PlaceShip(carrier);
        board.PlaceShip(destroyer1);
        board.PlaceShip(destroyer2);
        board.PlaceShip(gunship1);
        board.PlaceShip(gunship2);
        board.PlaceShip(gunship3);
        board.PlaceShip(gunship4);

        string boardPrinted = board.Print();

        boardPrinted.Should().Be(expectedBoard);
    }


}

public static class BoardTestExtensions
{
    public static void AssertAllCellsAreEmpty(this Board board)
    {
        for (int row = 0; row < Board.Rows; row++)
        for (int column = 0; column < Board.Columns; column++)
                board.Cells[column, row].Should().Be(Board.EmptyCell);
    }
}