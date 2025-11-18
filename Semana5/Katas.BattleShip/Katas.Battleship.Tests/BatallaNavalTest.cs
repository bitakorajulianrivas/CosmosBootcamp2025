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

        action.Should().Throw<ArgumentException>()
            .WithMessage("Solo se permiten 2 jugadores.");
    }

    [Fact]
    public void Si_inicioUnJuego_Debe_ExistirUnTableroParaCadaJugadorY2Jugadores()
    {
        var batallaNaval = CrearJuegoYAgregarJugadores();
        
        batallaNaval.Jugador1.Tablero.Should().NotBeNull();
        batallaNaval.Jugador2.Tablero.Should().NotBeNull();

        batallaNaval.Jugador1.Should().NotBeNull();
        batallaNaval.Jugador2.Should().NotBeNull();
    }

    [Fact]
    public void SI_InicioSinJugadores_Debe_LanzarExcepcion()
    {
        var batallaNaval = new BatallaNaval();
        var action = () => batallaNaval.Iniciar();

        action.Should().Throw<ArgumentException>()
            .WithMessage("No Estan los Jugadores Configurados.");
    }

    [Fact]
    public void Si_AgregoBarcosAlJuego_Debe_ExistirBarcosEnElTablero()
    {
        var batallaNaval = CrearJuegoYAgregarJugadores();
        
        batallaNaval.Jugador1.AgregarBarco(x:2 , y:2, tipo: "Gunship");

        batallaNaval.Jugador1.Tablero[2, 2].Should().Be('G');
    }
    
    private static BatallaNaval CrearJuegoYAgregarJugadores()
    {
        var batallaNaval = new BatallaNaval();
        var jugador1 = new Jugador("pollo");
        batallaNaval.AgregarJugador(jugador1);
        var jugador2 = new Jugador("gato");
        batallaNaval.AgregarJugador(jugador2);
        
        batallaNaval.Iniciar();
        
        return batallaNaval;
    }
}