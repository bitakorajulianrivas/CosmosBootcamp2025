namespace Katas.Battleship.Tests;

public class Jugador
{
    private const string YaExisteBarcoEnLaPosiciónEnviada = "Ya existe barco en la posición enviada.";
    public string Apodo { get; private set; }
    public char[,] Tablero { get; set; }

    public Jugador(string apodo)
    {
        Apodo = apodo;
        Tablero = new char[10, 10];
    }


    public void AgregarBarco(int x, int y, string tipo)
    {
        if (Tablero[x, y] != '\0')
            throw new ArgumentException(YaExisteBarcoEnLaPosiciónEnviada);
        
        Tablero[x, y] = 'G';
    }
}