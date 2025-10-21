namespace KatasTDD.Domain.BattleShips;

public class BattleShip
{
    public List<Player> Players { get; } = [];

    public void Start()
    {
        if (Players.Any() == false)
            throw new Exception("The game should start with 2 players.");
    }

    public void AddPlayer(Player player)
    {
        if (Players.Count >= 2)
            throw new Exception("Maximum 2 players for now.");

        Players.Add(player);
    }
}