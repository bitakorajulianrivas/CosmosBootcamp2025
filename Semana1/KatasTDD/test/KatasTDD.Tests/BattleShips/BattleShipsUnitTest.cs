using FluentAssertions;

namespace KatasTDD.Tests.BattleShips;

public class BattleShipsUnitTest
{
    private readonly PlayerUnitTest _playerUnitTest = new PlayerUnitTest();

    [Fact]
    public void Start_IfThereAreNoPlayers_ShouldThrowException()
    {
        Action action = () => new BattleShip(null)
            .Start();

        action.Should().ThrowExactly<Exception>()
            .WithMessage("The game should start with 2 players.");
    }

    [Fact]
    public void Start_IfThereAreTwoPlayers_ShouldNotThrowException()
    {
        var battleShip = new BattleShip(
            player2: new Player("MarinePhilipe"));

        battleShip.AddPlayer1(new Player("CaptainAugustus"));
        Action action = () => battleShip.Start();

        action.Should().NotThrow();
    }

    [Fact]
    public void AddPlayer1_ShouldAddPlayerNumber1()
    {
        var battleShip = new BattleShip(player2: new Player("MarinePhilipe"));

        battleShip.AddPlayer1(new Player("CaptainAugustus"));
        battleShip.Start();

        battleShip.Player1.Nickname.Should().Be("CaptainAugustus");
    }
}

public class BattleShip
{
    private readonly Player _player2;

    public Player Player1 { get; private set; }

    public BattleShip(Player player2)
    {
        _player2 = player2;
    }

    public void Start()
    {
        if (Player1 == null || _player2 == null)
            throw new Exception("The game should start with 2 players.");
    }

    public void AddPlayer1(Player player)
    {
        Player1 = player;
    }
}