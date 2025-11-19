namespace Katas.Battleship.Tests;

public class BatallaNavalBuilder
{
    private readonly BatallaNaval _batallaNaval = new();

    public BatallaNavalBuilder AgregarJugador(string apodo)
    {
        var jugador1 = new Jugador(apodo);
        _batallaNaval.AgregarJugador(jugador1);
        return this;
    }

    public BatallaNavalBuilder AgregarBarcosJugador1(Barco[] barcos)
    {
        foreach (var barco in barcos)
            _batallaNaval.Jugador1.AgregarBarco(barco);

        return this;
    }

    public BatallaNavalBuilder AgregarBarcosJugador2(Barco[] barcos)
    {
        foreach (var barco in barcos)
            _batallaNaval.Jugador2.AgregarBarco(barco);

        return this;
    }

    public BatallaNavalBuilder ValidarJugadores()
    {
        _batallaNaval.ValidarQueExistanDosJugadores();
        return this;
    }

    public BatallaNaval Construir() => _batallaNaval;
}