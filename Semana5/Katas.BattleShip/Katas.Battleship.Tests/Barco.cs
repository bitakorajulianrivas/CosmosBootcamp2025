namespace Katas.Battleship.Tests;

public class Barco
{
    public static Barco Gunship() => new Barco("Gunship", 1, 'G');
    public static Barco Destroyer() => new Barco("Destroyer", 3, 'D');
    public static Barco Carrier() => new Barco("Carrier", 4, 'C');
        
    private Barco(string tipo, int tamanio, char letra)
    {
        Tipo = tipo;
        Tamanio = tamanio;
        Letra = letra;
    }
        
    public string Tipo { get; }
    public int Tamanio { get; }
    public char Letra { get; }
}