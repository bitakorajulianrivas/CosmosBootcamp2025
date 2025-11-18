namespace Katas.Battleship.Tests;

public class Jugador
{
    private const string YaExisteBarcoEnLaPosiciónEnviada = "Ya existe barco en la posición enviada.";
    public string Apodo { get; private set; }
    public char[,] Tablero { get; set; }
    public int CantidadCarrier { get; set; }

    public Jugador(string apodo)
    {
        Apodo = apodo;
        Tablero = new char[10, 10];
        CantidadCarrier = 0;
    }


    public void AgregarBarco(Barco barco, int x, int y, bool esVertical = false)
    {
        if (Tablero[x, y] != '\0')
            throw new ArgumentException(YaExisteBarcoEnLaPosiciónEnviada);
        if (CantidadCarrier >= barco.CantidadBarcos)
            throw new ArgumentException("Solo se puede Asignar un Carrier");
        AsignarBarco(barco, x, y, esVertical);
    }

    private void AsignarBarco(Barco barco, int x, int y, bool esVertical)
    {
        for (int indice = 0; indice < barco.Tamanio; indice++)
        {
            if (esVertical)
                Tablero[x, y + indice] = barco.Letra;
            else
                Tablero[x + indice, y] = barco.Letra;
        }

        CantidadCarrier++;
    }
}