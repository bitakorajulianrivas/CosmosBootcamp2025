namespace Katas.Battleship.Tests;

public class Barco
{
    public static Barco Gunship() => new Barco("Gunship", 1, 'G',4);
    public static Barco Destroyer() => new Barco("Destroyer", 3, 'D',2);
    public static Barco Carrier() => new Barco("Carrier", 4, 'C',1);
        
    private Barco(string tipo, int tamanio, char letra, int cantidadBarcos)
    {
        Tipo = tipo;
        Tamanio = tamanio;
        Letra = letra;
        CantidadBarcos = cantidadBarcos;
    }
        
    public string Tipo { get; }
    public int Tamanio { get; }
    public char Letra { get; }

    public int CantidadBarcos { get; private set; }
    
}