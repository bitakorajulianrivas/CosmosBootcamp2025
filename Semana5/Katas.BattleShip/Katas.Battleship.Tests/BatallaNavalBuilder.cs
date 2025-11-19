namespace Katas.Battleship.Tests;

public class BatallaNavalBuilder
{
    private readonly BatallaNaval _batallaNaval = new();

    public BatallaNavalBuilder()
    {
        _batallaNaval = new BatallaNaval();
    }

    public BatallaNavalBuilder AgregarJugador(string apodo)
    {
        var jugador1 = new Jugador(apodo);
        _batallaNaval.AgregarJugador(jugador1);
        return this;
    }

    public BatallaNavalBuilder AgregarBarcosJugador1((Barco barco, Posicion posicion)[] barcoPosiciones)
    {
        foreach (var barcoPosicion in barcoPosiciones)
            _batallaNaval.Jugador1.AgregarBarco(barcoPosicion.barco, barcoPosicion.posicion);

        return this;
    }

    public BatallaNavalBuilder AgregarBarcosJugador2((Barco barco, Posicion posicion)[] barcoPosiciones)
    {
        foreach (var barcoPosicion in barcoPosiciones)
            _batallaNaval.Jugador2.AgregarBarco(barcoPosicion.barco, barcoPosicion.posicion);

        return this;
    }

    public BatallaNavalBuilder ValidarJugadores()
    {
        _batallaNaval.ValidarExistenJugadores();
        return this;
    }

    public BatallaNaval Construir() => _batallaNaval;
}