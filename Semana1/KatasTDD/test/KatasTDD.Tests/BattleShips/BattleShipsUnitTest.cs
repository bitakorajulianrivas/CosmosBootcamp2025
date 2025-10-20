using FluentAssertions;

namespace KatasTDD.Tests.BattleShips;

public class BattleShipsUnitTest
{
    [Fact]
    public void Start_IfThereAreNoPlayers_ShouldThrowException()
    {
        Action action = () => new BattleShip(null, null).Start();

        action.Should().ThrowExactly<Exception>()
            .WithMessage("The game should start with 2 players.");
    }

    [Fact]
    public void Start_IfThereAreTwoPlayers_ShouldNotThrowException()
    {
        var battleShip = new BattleShip(
            player1: new Player(),
            player2: new Player());

        Action action = () => battleShip.Start();

        action.Should().NotThrow();
    }
}

public class BattleShip(Player player1, Player player2)
{
    public void Start()
    {
        if (player1 == null || player2 == null)
            throw new Exception("The game should start with 2 players.");
    }
}

public class Player { }