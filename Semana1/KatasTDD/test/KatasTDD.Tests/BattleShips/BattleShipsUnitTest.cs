using FluentAssertions;

namespace KatasTDD.Tests.BattleShips;

public class BattleShipsUnitTest
{
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
        battleShip.AddPlayer(new Player("MarinePhilipe"));
        Action action = () => battleShip.Start();

        action.Should().NotThrow();
    }

    [Fact]
    public void AddPlayer_ShouldAddPlayerToTheList()
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

    [Fact]
    public void AddPlayer_IfReceiveMoreThan3Players_ShouldThrowAnException()
    {
        var battleShip = new BattleShip();

        battleShip.AddPlayer(new Player("CaptainAugustus"));
        battleShip.AddPlayer(new Player("MarinePhilipe"));

        Action action = () => battleShip.AddPlayer(new Player("CadetCamille"));

        action.Should().ThrowExactly<Exception>()
            .WithMessage("Maximum 2 players for now.");
    }
}

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