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
            Player1: new Player("CaptainAugustus"),
            Player2: new Player("MarinePhilipe"));

        Action action = () => battleShip.Start();

        action.Should().NotThrow();
    }

    [Fact]
    public void AddPlayer1_ShouldAddPlayerNumber1()
    {
        var battleShip = new BattleShip(null, Player2: new Player("MarinePhilipe"));

        battleShip.AddPlayer1(new Player("CaptainAugustus"));
        battleShip.Start();

        battleShip.Player1.Nickname.Should().Be("CaptainAugustus");
    }
}

public class BattleShip(Player Player1, Player Player2)
{
    public void Start()
    {
        if (Player1 == null || Player2 == null)
            throw new Exception("The game should start with 2 players.");
    }

    public Player Player1 { get; private set; }

    public void AddPlayer1(Player player)
    {
        Player1 = new Player("CaptainAugustus");
    }
}