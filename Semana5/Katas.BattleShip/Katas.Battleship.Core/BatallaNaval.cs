namespace Katas.Battleship.Core;

public class BatallaNaval
{
    private readonly List<Jugador> _jugadores = [];
    private bool _juegoIniciado;
    private string? _jugadorGanador;
    
    public bool HaFinalizado;
    public bool EsTurnoPrincipal = true;

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

        (char disparo, Barco? barco) disparo = ObtenerJugadorOponente().RecibirDisparo(x, y);
        ObtenerJugadorActual().RegistrarDisparo(x, y, disparo.disparo, disparo.barco);

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
        {
            HaFinalizado = true;
            _jugadorGanador = ObtenerJugadorActual().Apodo;
        }

        EsTurnoPrincipal = !EsTurnoPrincipal;
    }
    
    public string Imprimir(bool esReporte = false)
    {
        if (HaFinalizado)
            return  MostrarJugadorGanador() + 
                    ObtenerJugadorActual().Imprimir(esReporte: true) +
                    ObtenerJugadorActual().Imprimir(esReporte: false) +
                    ObtenerJugadorOponente().Imprimir(esReporte: true) +
                    ObtenerJugadorOponente().Imprimir(esReporte: false);
        
        return ObtenerJugadorActual().Imprimir(esReporte);
    }

    public string ImprimirTableroDeDisparos() => 
        ObtenerJugadorActual().ImprimirTableroDeDisparos();

    public string ApodoJugadorActual => ObtenerJugadorActual().Apodo;
    
    public string MostrarJugadorGanador() => 
        string.Format(BatallaNavalMensajes.MensajeJugadorGanador, _jugadorGanador);

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
    private Jugador ObtenerJugadorActual() => EsTurnoPrincipal ? _jugadores[0] : _jugadores[1];
    private Jugador ObtenerJugadorOponente() => EsTurnoPrincipal ? _jugadores[1] : _jugadores[0];
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