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
}

public static class BoardTestExtensions
{
    public static void AssertAllCellsAreEmpty(this Board board)
    {
        for (int column = 0; column < Board.Columns; column++)
            for (int row = 0; row < Board.Columns; row++)
                board.Cells[column, row].Should().Be(Board.EmptyCell);
    }
}