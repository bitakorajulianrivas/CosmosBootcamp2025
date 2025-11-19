namespace Katas.Battleship.Tests;

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

    public void ValidarExistenJugadores()
    {
        if (!ExisteJugador1() || !ExisteJugador2())
            throw new ArgumentException(NoEstanLosJugadoresConfigurados);
    }

    public void Iniciar()
    {
        Jugador1.ValidarBarcos();
        Jugador2.ValidarBarcos();
    }


    public string Imprimir(string apodo)
    {
        Jugador jugador = ObtenerJugador(apodo);

        return jugador.ImprimirTablero();
    }

    private Jugador ObtenerJugador(string apodo)
    {
        if (Jugador1.Apodo == apodo)
            return Jugador1;
        
        return Jugador2;
    }
}