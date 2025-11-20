namespace Katas.Battleship.Tests;

public class BatallaNavalBuilder
{
    private readonly BatallaNaval _batallaNaval = new();

    public BatallaNavalBuilder AgregarJugador(string apodo)
    {
        var jugador = new Jugador(apodo);
        _batallaNaval.AgregarJugador(jugador);
        return this;
    }

    public BatallaNavalBuilder AgregarBarcosJugador1(Barco[] barcos)
    {
        foreach (var barco in barcos)
            _batallaNaval.ObtenerJugadorActual().AgregarBarco(barco);

        return this;
    }

    public BatallaNavalBuilder AgregarBarcosJugador2(Barco[] barcos)
    {
        foreach (var barco in barcos)
            _batallaNaval.ObtenerJugadorOponente().AgregarBarco(barco);

        return this;
    }

    public BatallaNaval Construir() => _batallaNaval;
}