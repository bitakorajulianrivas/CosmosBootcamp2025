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
    
    [Fact]
    public void Si_AgregoJugador2_Debe_ExistirJugador2()
    {
        var jugador2 = new Jugador("gato");
        var batallaNaval = new BatallaNaval();
            
        batallaNaval.AgregarJugador(jugador2);
        
        batallaNaval.Jugador2.Apodo.Should().Be("gato");
    }
}

public class BatallaNaval
{
    public Jugador Jugador1 { get; private set; }
    public Jugador Jugador2 { get; private set; }

    public void AgregarJugador(Jugador jugador)
    {
        Jugador1 ??= jugador;

        Jugador2 = jugador;
    }
    
}

public class Jugador
{
    public string Apodo { get; private set; }

    public Jugador(string apodo)
    {
       Apodo = apodo;
    }
}