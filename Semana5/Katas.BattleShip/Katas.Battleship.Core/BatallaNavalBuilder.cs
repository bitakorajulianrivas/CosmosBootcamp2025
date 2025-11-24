namespace Katas.Battleship.Core;

public class BatallaNavalBuilder
{
    private readonly BatallaNaval _batallaNaval = new();

    public BatallaNavalBuilder AgregarJugador(string apodo, Barco[] barcos)
    {
        var jugador = new Jugador(apodo);
        
        foreach (var barco in barcos)
            jugador.AgregarBarco(barco);
        
        _batallaNaval.AgregarJugador(jugador);
        return this;
    }
    
    public BatallaNavalBuilder AgregarJugador(string apodo)
    {
        var jugador = new Jugador(apodo);
        
        _batallaNaval.AgregarJugador(jugador);
        return this;
    }
    
    public BatallaNavalBuilder AgregarJugador(Jugador jugador)
    {
        _batallaNaval.AgregarJugador(jugador);
        return this;
    }

    public BatallaNaval Construir() => _batallaNaval;
}