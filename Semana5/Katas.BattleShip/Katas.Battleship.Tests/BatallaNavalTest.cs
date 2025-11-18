using AwesomeAssertions;

namespace Katas.Battleship.Tests;

public class BatallaNavalTest
{
    [Fact]
    public void SI_AgregoJugador1_Debe_ExistirJugador1()
    {
        var jugador1 = new Jugador("pollo");
        var batallaNaval = new BatallaNaval();
        
        batallaNaval.AgregarJugador(jugador1);
        
        batallaNaval.Jugador1.Apodo.Should().Be("pollo");
    }
}

public class BatallaNaval
{
    public void AgregarJugador(Jugador jugador)
    {
        Jugador1 = jugador;
    }

    public Jugador Jugador1 { get; set; }
}

public class Jugador
{
    public Jugador(string apodo)
    {
       Apodo = apodo;
    }

    public string Apodo { get; set; }
}