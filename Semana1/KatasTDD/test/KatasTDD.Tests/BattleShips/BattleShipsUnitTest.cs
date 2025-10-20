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

        battleShip.AddPlayer(new Player("CaptainAugustus"));
        battleShip.AddPlayer2(new Player("MarinePhilipe"));
        Action action = () => battleShip.Start();

        action.Should().NotThrow();
    }

    [Fact]
    public void AddPlayer_ShouldAddPlayerNumber1()
    {
        var battleShip = new BattleShip();

        battleShip.AddPlayer(new Player("CaptainAugustus"));

        battleShip.Players.First().Nickname.Should().Be("CaptainAugustus");
    }

    [Fact]
    public void AddPlayer_IfAddPlayerOneAndPlayerTwo_ShouldBeAListOfTwoPlayers()
    {
        var battleShip = new BattleShip();

        battleShip.AddPlayer(new Player("CaptainAugustus"));
        battleShip.AddPlayer(new Player("MarinePhilipe"));

        battleShip.Players.Should().HaveCount(2);
        battleShip.Players.First().Nickname.Should().Be("CaptainAugustus");
        battleShip.Players.Last().Nickname.Should().Be("MarinePhilipe");
    }
}

public class BattleShip
{
    public Player? Player2 { get; private set; }
    public List<Player> Players { get; set; }

    public void Start()
    {
        if (Players == null || Player2 == null)
            throw new Exception("The game should start with 2 players.");
    }

    public void AddPlayer2(Player player)
    {
        Player2 = player;
    }

    public void AddPlayer(Player player)
    {
        Players = new List<Player>() { player };
    }
}