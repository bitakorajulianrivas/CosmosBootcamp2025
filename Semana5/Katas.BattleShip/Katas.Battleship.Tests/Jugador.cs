namespace Katas.Battleship.Tests;

public class Jugador
{
    public string Apodo { get; private set; }
    public char[,] Tablero { get; set; }

    public Jugador(string apodo)
    {
        Apodo = apodo;
        Tablero =  new char[10, 10];
    }


    public void AgregarBarco(int x, int y, string tipo)
    {
        throw new NotImplementedException();
    }
}