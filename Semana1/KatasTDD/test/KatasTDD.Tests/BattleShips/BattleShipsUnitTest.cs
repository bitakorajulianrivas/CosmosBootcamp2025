using FluentAssertions;

namespace KatasTDD.Tests.BattleShips;

public class BattleShipsUnitTest
{
    private readonly PlayerUnitTest _playerUnitTest = new PlayerUnitTest();

    [Fact]
    public void Start_IfThereAreNoPlayers_ShouldThrowException()
    {
        Action action = () => new BattleShip()
            .Start();

        action.Should().ThrowExactly<Exception>()
            .WithMessage("The game should start with 2 players.");
    }

    [Fact]
    public void Start_IfThereAreTwoPlayers_ShouldNotThrowException()
    {
        var battleShip = new BattleShip();

        battleShip.AddPlayer1(new Player("CaptainAugustus"));
        battleShip.AddPlayer2(new Player("MarinePhilipe"));
        Action action = () => battleShip.Start();

        action.Should().NotThrow();
    }

    [Fact]
    public void AddPlayer_ShouldAddPlayerNumber1()
    {
        var battleShip = new BattleShip();

        battleShip.AddPlayer(new Player("CaptainAugustus"));

        battleShip.Player1.Nickname.Should().Be("CaptainAugustus");
    }

    [Fact]
    public void AddPlayer2_ShouldAddPlayerNumber2()
    {
        var battleShip = new BattleShip();

        battleShip.AddPlayer2(new Player("MarinePhilipe"));

        battleShip.Player2.Nickname.Should().Be("MarinePhilipe");
    }
}

public class BattleShip
{
    public Player? Player1 { get; private set; }
    public Player? Player2 { get; private set; }

    public void Start()
    {
        if (Player1 == null || Player2 == null)
            throw new Exception("The game should start with 2 players.");
    }

    public void AddPlayer1(Player? player)
    {
        Player1 = player;
    }

    public void AddPlayer2(Player player)
    {
        Player2 = player;
    }

    public void AddPlayer(Player player)
    {
        throw new NotImplementedException();
    }
}