namespace Katas.Battleship.Tests;

public class BatallaNaval
{
    private bool _esTurnoPrincipal = true;
    private bool _juegoIniciado;
    private const string NoEstanLosJugadoresConfigurados = "No Estan los Jugadores Configurados.";
    private const string SoloSePermitenJugadores = "Solo se permiten 2 jugadores.";
    private const string NoPuedeDispararSinIniciarElJuego = "No puede disparar sin iniciar el juego.";
    private const string MensajeDisparoFallido = "Disparo fallido en la posicion ({0}, {1})";
    private const string MensajeDisparoAcertado = "Disparo acertado en la posicion ({0}, {1})";
    private const string NoPuedeDispararEnUnaMismaPosición = "No puede disparar en una misma posición.";

    private readonly List<Jugador> _jugadores = [];
    
   
    public void AgregarJugador(Jugador jugador)
    {
        ValidarQueNoPuedaAgregarMasDeDosJugadores();

        _jugadores.Add(jugador);
    }
    public void Iniciar()
    {
        ValidarQueExistanDosJugadores();
        _jugadores[0].ValidarQueExistanSieteBarcosAsignadosPorTablero();
        _jugadores[1].ValidarQueExistanSieteBarcosAsignadosPorTablero();
        _juegoIniciado = true;
        _esTurnoPrincipal = true;
    }
    public string Disparar(int x, int y)
    {
        if (!_juegoIniciado)
            throw new ArgumentException(NoPuedeDispararSinIniciarElJuego);

        if (ObtenerJugadorActual().TieneDisparo(x, y))
            throw new ArgumentException(NoPuedeDispararEnUnaMismaPosición);

        char disparo = ObtenerJugadorOponente().RecibirDisparo(x, y);
        ObtenerJugadorActual().RegistrarDisparo(x, y, disparo);

        EstadoDisparo estadoDisparo = ObtenerJugadorOponente().ObtenerEstadoDisparo();

        return estadoDisparo switch
        {
            EstadoDisparo.DisparoAcertado => string.Format(MensajeDisparoAcertado, x, y),
            EstadoDisparo.DisparoFallido => string.Format(MensajeDisparoFallido, x, y),
            EstadoDisparo.BarcoHundido => MostrarMensajeBarcoDerribado(x, y, estadoDisparo),
            _ => throw new NotImplementedException()
        };
    }
    public void FinalizarTurno()
    {
        _esTurnoPrincipal = !_esTurnoPrincipal;
    }
    public string Imprimir(string apodo)
    {
        Jugador jugador = ObtenerJugador(apodo);

        return jugador.ImprimirTablero();
    }
    public string ObtenerInformePorJugador(string apodo)
    {
        return ObtenerJugador(apodo).ObtenerInforme();
    }
    public Jugador ObtenerJugadorActual() => _esTurnoPrincipal ? _jugadores[0] : _jugadores[1];
    public Jugador ObtenerJugadorOponente() => _esTurnoPrincipal ? _jugadores[1] : _jugadores[0];
    
    private void ValidarQueExistanDosJugadores()
    {
        if (_jugadores.Count < 2)
            throw new ArgumentException(NoEstanLosJugadoresConfigurados);
    }
    private void ValidarQueNoPuedaAgregarMasDeDosJugadores()
    {
        if (_jugadores.Count == 2)
            throw new ArgumentException(SoloSePermitenJugadores);
    }
    private Jugador ObtenerJugador(string apodo)
    {
        return _jugadores.First(jugador => jugador.Apodo == apodo);
    }
    private string MostrarMensajeBarcoDerribado(int x, int y, EstadoDisparo estadoDisparo)
    {
        if (estadoDisparo == EstadoDisparo.BarcoHundido)
        {
            Barco? barcoDerribado = ObtenerJugadorOponente().ObtenerBarco(x, y);

            return $"Se ha hundido el barco {barcoDerribado?.Tipo} " +
                   $"({barcoDerribado?.Posicion.EjeX}, " +
                   $"{barcoDerribado?.Posicion.EjeY})";
        }

        return string.Empty;
    }
}