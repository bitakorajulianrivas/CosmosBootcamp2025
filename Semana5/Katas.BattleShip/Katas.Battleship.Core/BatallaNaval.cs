namespace Katas.Battleship.Tests;

public class BatallaNaval
{
    private readonly List<Jugador> _jugadores = [];
    private bool _esTurnoPrincipal = true;
    private bool _juegoIniciado;
    private bool _juegoFinalizado;
    
    public void AgregarJugador(Jugador jugador)
    {
        ValidarQueNoPuedaAgregarMasDeDosJugadores();

        _jugadores.Add(jugador);
    }
    public void Iniciar()
    {
        ValidarQueExistanDosJugadores();
        ObtenerJugadorActual()
            .ValidarQueExistanSieteBarcosAsignadosPorTablero();
        ObtenerJugadorOponente()
            .ValidarQueExistanSieteBarcosAsignadosPorTablero();
        
        _juegoIniciado = true;
    }
    public string Disparar(int x, int y)
    {
        if (!_juegoIniciado)
            throw new ArgumentException(BatallaNavalMensajes.NoPuedeDispararSinIniciarElJuego);

        if (ObtenerJugadorActual().TieneDisparo(x, y))
            throw new ArgumentException(BatallaNavalMensajes.NoPuedeDispararEnUnaMismaPosición);

        char disparo = ObtenerJugadorOponente().RecibirDisparo(x, y);
        ObtenerJugadorActual().RegistrarDisparo(x, y, disparo);

        EstadoDisparo estadoDisparo = ObtenerJugadorOponente().ObtenerEstadoDisparo();

        return estadoDisparo switch
        {
            EstadoDisparo.DisparoAcertado => string.Format(BatallaNavalMensajes.MensajeDisparoAcertado, x, y),
            EstadoDisparo.DisparoFallido => string.Format(BatallaNavalMensajes.MensajeDisparoFallido, x, y),
            EstadoDisparo.BarcoHundido => MostrarMensajeBarcoDerribado(x, y, estadoDisparo),
            _ => throw new NotImplementedException()
        };
    }
    public void FinalizarTurno()
    {
        if (ObtenerJugadorOponente().TieneTodosLosBarcosDerribados())
            _juegoFinalizado = true;
        
        _esTurnoPrincipal = !_esTurnoPrincipal;
    }
    public string Imprimir(bool esReporte = false)
    {
        if (_juegoFinalizado)
            return ObtenerJugadorActual().Imprimir(esReporte: true) +
                   ObtenerJugadorActual().Imprimir(esReporte: false) +
                   ObtenerJugadorOponente().Imprimir(esReporte: true) +
                   ObtenerJugadorOponente().Imprimir(esReporte: false);
        
        return ObtenerJugadorActual().Imprimir(esReporte);
    }
    
    
    private void ValidarQueExistanDosJugadores()
    {
        if (_jugadores.Count < 2)
            throw new ArgumentException(BatallaNavalMensajes.NoEstanLosJugadoresConfigurados);
    }
    private void ValidarQueNoPuedaAgregarMasDeDosJugadores()
    {
        if (_jugadores.Count == 2)
            throw new ArgumentException(BatallaNavalMensajes.SoloSePermitenJugadores);
    }
    private Jugador ObtenerJugadorActual() => _esTurnoPrincipal ? _jugadores[0] : _jugadores[1];
    private Jugador ObtenerJugadorOponente() => _esTurnoPrincipal ? _jugadores[1] : _jugadores[0];
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