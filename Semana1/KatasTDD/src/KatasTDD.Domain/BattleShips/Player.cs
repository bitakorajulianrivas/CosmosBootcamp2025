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

    public void PlaceShipOnBoard(Ship ship)
    {
        if (ShipsPlacedPerType[ship.ShipType] >= ship.GetMaxShipsPerType())
            throw new Exception($"All {ship.ShipType.ToString().ToLower()}s have been placed on the board.");

        Board.PlaceShip(ship);
        ShipsPlacedPerType[ship.ShipType]++;
    }
}