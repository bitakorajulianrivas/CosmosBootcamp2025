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
        var batallaNaval = new BatallaNaval();
        var jugador1 = new Jugador("pollo");
        batallaNaval.AgregarJugador(jugador1);
        var jugador2 = new Jugador("gato");
        batallaNaval.AgregarJugador(jugador2);

        batallaNaval.Iniciar();
        
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
}

public class BatallaNaval
{
    private const string NoEstanLosJugadoresConfigurados = "No Estan los Jugadores Configurados.";
    private const string SoloSePermitenJugadores = "Solo se permiten 2 jugadores.";
    public Jugador Jugador1 { get; private set; }
    public Jugador Jugador2 { get; private set; }

    public void AgregarJugador(Jugador jugador)
    {
        ValidarMaximo2Jugadores();

        if (Jugador1 == null)
            Jugador1 = jugador;
        else
            Jugador2 = jugador;
    }

    private void ValidarMaximo2Jugadores()
    {
        if (ExisteJugador1() && ExisteJugador2())
            throw new ArgumentException(SoloSePermitenJugadores);
    }

    private bool ExisteJugador2() => Jugador2 != null;

    private bool ExisteJugador1() => Jugador1 != null;

    public void Iniciar()
    {
        ValidarExistenJugadores();
    }

    private void ValidarExistenJugadores()
    {
        if (!ExisteJugador1() || !ExisteJugador2())
            throw new ArgumentException(NoEstanLosJugadoresConfigurados);
    }
}

public class Jugador
{
    public string Apodo { get; private set; }
    public char[,] Tablero { get; set; }

    public Jugador(string apodo)
    {
        Apodo = apodo;
        Tablero =  new char[10, 10];
    }
}