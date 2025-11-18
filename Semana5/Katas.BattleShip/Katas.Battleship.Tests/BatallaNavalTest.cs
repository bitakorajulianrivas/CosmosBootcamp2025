
using FluentAssertions;

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
        var batallaNaval = new BatallaNaval();

        var jugador1 = new Jugador("pollo");
        var jugador2 = new Jugador("gato");
        batallaNaval.AgregarJugador(jugador1);
        
        batallaNaval.AgregarJugador(jugador2);
        
        batallaNaval.Jugador2.Apodo.Should().Be("gato");
    }

    [Fact]
    public void Si_AgregoJugador3_Debe_LanzarExcepcion()
    {
        var batallaNaval = new BatallaNaval();
        var jugador1 = new Jugador("pollo");
        batallaNaval.AgregarJugador(jugador1);
        var jugador2 = new Jugador("gato");
        batallaNaval.AgregarJugador(jugador2);
        var jugador3 = new Jugador("perro");
        
        var action = () => batallaNaval.AgregarJugador(jugador3);
        
        action.Should().Throw<ArgumentException>().WithMessage("Error");
    }
}

public class BatallaNaval
{
    public Jugador Jugador1 { get; private set; }
    public Jugador Jugador2 { get; private set; }

    public void AgregarJugador(Jugador jugador)
    {
        if(Jugador1 is not null && Jugador2 is not null)
            throw new ArgumentException("Error");
        
        if (Jugador1 is null)
        {
            Jugador1 = jugador;
        }
        else
        {
            Jugador2 = jugador;
        }
        
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