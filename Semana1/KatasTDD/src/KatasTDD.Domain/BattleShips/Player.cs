using KatasTDD.Domain.BattleShips.Enums;

namespace KatasTDD.Domain.BattleShips;

public class Player
{
    public string Nickname { get; }
    public Board Board { get; }
    public Dictionary<ShipType, int> ShipsPlacedPerType { get; set; }

    public Player(string nickname)
    {
        Nickname = nickname;
        Board = new Board();
        ShipsPlacedPerType = new Dictionary<ShipType, int>()
        {
            {ShipType.Carrier, 0},
            {ShipType.Destroyer, 0},
            {ShipType.Gunship, 0}
        };
    }

    public Dictionary<ShipType, int> ShipTypes { get; } = new () {
        {ShipType.Carrier, 1},
        {ShipType.Destroyer, 2},
        {ShipType.Gunship, 4} };

    public void PlaceShipOnBoard(Ship ship)
    {
        if (ship.ShipType == ShipType.Carrier && ShipsPlacedPerType[ShipType.Carrier] >= 1)
            throw new Exception("All carriers have been placed on the board.");

        Board.PlaceShip(ship);
        ShipsPlacedPerType[ship.ShipType] = 1;
    }
}