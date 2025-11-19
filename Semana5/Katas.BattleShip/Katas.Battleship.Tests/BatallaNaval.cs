namespace Katas.Battleship.Tests;

public class BatallaNaval
{
    private bool _esTurnoPrincipal = true;
    private bool _juegoIniciado = false;
    private const string NoEstanLosJugadoresConfigurados = "No Estan los Jugadores Configurados.";
    private const string SoloSePermitenJugadores = "Solo se permiten 2 jugadores.";
    private const string? NoPuedeDispararSinIniciarElJuego = "No puede disparar sin iniciar el juego.";

    public Jugador Jugador1 { get; private set; }
    public Jugador Jugador2 { get; private set; }
    
    public void AgregarJugador(Jugador jugador)
    {
        ValidarQueExistanSoloDosJugadores();

        if (Jugador1 == null)
            Jugador1 = jugador;
        else
            Jugador2 = jugador;
    }

    private void ValidarQueExistanSoloDosJugadores()
    {
        if (ExisteJugador1() && ExisteJugador2())
            throw new ArgumentException(SoloSePermitenJugadores);
    }

    private bool ExisteJugador2() => Jugador2 != null;

    private bool ExisteJugador1() => Jugador1 != null;

    public void ValidarQueExistanDosJugadores()
    {
        if (!ExisteJugador1() || !ExisteJugador2())
            throw new ArgumentException(NoEstanLosJugadoresConfigurados);
    }

    public void Iniciar()
    {
        Jugador1.ValidarQueExistanSieteBarcosAsignadosPorTablero();
        Jugador2.ValidarQueExistanSieteBarcosAsignadosPorTablero();
        _juegoIniciado = true;
        _esTurnoPrincipal = true;
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

    public void Disparar(int x, int y)
    {
        if (!_juegoIniciado)
            throw new ArgumentException(NoPuedeDispararSinIniciarElJuego);
        
        char disparo = ObtenerJugadorOponente().RecibirDisparo(x, y);
        ObtenerJugadorActual().RegistrarDisparo(x, y, disparo);
    }

    public void FinalizarTurno() => _esTurnoPrincipal = !_esTurnoPrincipal;

    public Jugador ObtenerJugadorActual() => _esTurnoPrincipal ? Jugador1: Jugador2;

    public Jugador ObtenerJugadorOponente() => _esTurnoPrincipal ? Jugador2: Jugador1;
}