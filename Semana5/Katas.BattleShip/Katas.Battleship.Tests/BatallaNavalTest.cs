using AwesomeAssertions;

namespace Katas.Battleship.Tests;

public class BatallaNavalTest
{
    [Fact]
    public void SI_AgregoJugador_Debe_ExistirJugador()
    {
        var apodo = "pollo";
        var jugador = new Jugador(apodo);

        var batallaNaval = new BatallaNaval();
        batallaNaval.AgregarJugador(jugador);
        
        batallaNaval.Jugador1.Apodo.Should().Be("pollo");

    }
}

public class BatallaNaval
{
    public void AgregarJugador(Jugador jugador)
    {
        throw new NotImplementedException();
    }

    public Jugador Jugador1 { get; set; }
}

public class Jugador
{
    public Jugador(string apodo)
    {
        throw new NotImplementedException();
    }

    public string Apodo { get; set; }
}