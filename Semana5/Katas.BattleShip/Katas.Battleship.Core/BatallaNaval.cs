namespace Katas.Battleship.Core;

public class BatallaNaval : IBatallaNaval
{
    private readonly List<Jugador> _jugadores = [];
    public bool EsTurnoPrincipal = true;
    private bool _juegoIniciado;
    public bool JuegoFinalizado;
    private string? _jugadorGanador;

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
        {
            JuegoFinalizado = true;
            _jugadorGanador = ObtenerJugadorActual().Apodo;
        }

        EsTurnoPrincipal = !EsTurnoPrincipal;
    }
    
    public string Imprimir(bool esReporte = false)
    {
        if (JuegoFinalizado)
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
    
    private string MostrarJugadorGanador() => 
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

public interface IBatallaNaval
{
    void AgregarJugador(Jugador jugador);
    void Iniciar();
    string Disparar(int x, int y);
    void FinalizarTurno();
    string Imprimir(bool esReporte = false);
}