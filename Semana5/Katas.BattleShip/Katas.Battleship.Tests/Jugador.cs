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

        if (tipo == "Destroyer")
        {
            if (esVertical)
            {
                Tablero[x, y] = 'D';
                Tablero[x, y + 1] = 'D';
                Tablero[x, y + 2] = 'D';
            }
            else
            {
                Tablero[x, y] = 'D';
                Tablero[x + 1, y] = 'D';
                Tablero[x + 2, y] = 'D';
            }
        }

        if (tipo == "Carrier")
        {
            if (esVertical)
            {
                Tablero[x, y] = 'C';
                Tablero[x, y + 1] = 'C';
                Tablero[x, y + 2] = 'C';
                Tablero[x, y + 3] = 'C';
            }
            else
            {
                Tablero[x, y] = 'C';
                Tablero[x + 1, y] = 'C';
                Tablero[x + 2, y] = 'C';
                Tablero[x + 3, y] = 'C';
            }
        }
    }
}