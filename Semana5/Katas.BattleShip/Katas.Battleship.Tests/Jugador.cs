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


    public void AgregarBarco(int x, int y, string tipo, bool esVertical = false)
    {
        if (Tablero[x, y] != '\0')
            throw new ArgumentException(YaExisteBarcoEnLaPosiciónEnviada);

        if (tipo == "Gunship")
            Tablero[x, y] = 'G';
        else
        {
            if (esVertical is false)
            {
                Tablero[x, y] = 'D';
                Tablero[x + 1, y] = 'D';
                Tablero[x + 2, y] = 'D';
            }
            else
            {
                Tablero[x, y] = 'D';
                Tablero[x, y + 1] = 'D';
                Tablero[x, y + 2] = 'D';
            }
        }
    }
}