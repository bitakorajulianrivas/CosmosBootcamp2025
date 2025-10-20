using FluentAssertions;

namespace KatasTDD.Tests.BattleShips;

public class BattleShipsUnitTest
{
    private readonly PlayerUnitTest _playerUnitTest = new PlayerUnitTest();

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
            player1: new Player("CaptainAugustus"),
            player2: new Player("MarinePhilipe"));

        Action action = () => battleShip.Start();

        action.Should().NotThrow();
    }

    [Fact]
    public void AddPlayer1_ShouldAddPlayerNumber1()
    {
        var battleShip = new BattleShip(null, null);

        battleShip.AddPlayer1(new Player("CaptainAugustus"));
        battleShip.Start();

        battleShip.Player1.Nickname.Should().Be("CaptainAugustus");
    }
}

public class BattleShip(Player player1, Player player2)
{
    public void Start()
    {
        if (player1 == null || player2 == null)
            throw new Exception("The game should start with 2 players.");
    }

    public Player Player1 { get; }

    public void AddPlayer1(Player player)
    {
        throw new NotImplementedException();
    }
}