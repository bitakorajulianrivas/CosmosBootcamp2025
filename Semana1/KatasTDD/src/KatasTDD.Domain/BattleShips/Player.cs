using KatasTDD.Domain.BattleShips.Enums;

namespace KatasTDD.Domain.BattleShips;

public class Player(string nickname)
{
    public string Nickname { get; } = nickname;
    public Board Board => new ();

    public Dictionary<ShipType, int> ShipTypes { get; } = new () {
        {ShipType.Carrier, 1},
        {ShipType.Destroyer, 2},
        {ShipType.Gunship, 4} };

    public Dictionary<ShipType, int> ShipsPlacedPerType => new ()
    {
        {ShipType.Carrier, 0},
        {ShipType.Destroyer, 0},
        {ShipType.Gunship, 0}
    };

    public void PlaceShipOnBoard(Ship ship)
    {
        throw new NotImplementedException();
    }
}