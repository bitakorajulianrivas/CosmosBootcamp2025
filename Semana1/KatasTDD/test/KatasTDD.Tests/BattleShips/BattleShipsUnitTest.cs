using FluentAssertions;

namespace KatasTDD.Tests.BattleShips;

public class BattleShipsUnitTest
{
    [Fact]
    public void Start_IfThereAreNoPlayers_ShouldThrowException()
    {
        Action action = () => new BattleShip().Start();

        action.Should().ThrowExactly<Exception>()
            .WithMessage("The game should start with 2 players.");
    }

    [Fact]
    public void Start_IfThereAreTwoPlayers_ShouldNotThrowException()
    {
        var battleShip = new BattleShip();
        battleShip.AddPlayer(new Player());
        battleShip.AddPlayer(new Player());

        Action action = () => battleShip.Start();

        action.Should().NotThrow();
    }
}

public class BattleShip
{
    private readonly List<Player> _players = [];

    public void Start()
    {
        if (_players.Any() == false)
            throw new Exception("The game should start with 2 players.");
    }

    public void AddPlayer(Player player)
    {
        throw new NotImplementedException();
    }
}

public class Player { }