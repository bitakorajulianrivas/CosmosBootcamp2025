using FluentAssertions;

namespace KatasTDD.Tests.BattleShips;

public class BattleShipsUnitTest
{
    [Fact]
    public void Start_IfThereAreNoPlayers_ShouldThrowException()
    {
        Action action = () => new Game().Start();

        action.Should().ThrowExactly<Exception>()
            .WithMessage("The game should start with 2 players.");
    }
}

public class Game
{
    public void Start()
    {
        throw new Exception("The game should start with 2 players.");
    }
}